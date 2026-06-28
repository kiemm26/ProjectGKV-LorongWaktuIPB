using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;

[System.Serializable]
public class QuizQuestion
{
    public string questionText;
    public string[] options;
    public int correctAnswerIndex;
}

[System.Serializable]
public class QuizData
{
    public QuizQuestion[] questions;
}

public class QuizManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI[] optionTexts;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI feedbackText;
    public Button nextButton;
    public TextMeshProUGUI nextButtonText; // teks di dalam tombol Next, untuk ganti jadi "Selesai" di akhir

    [Header("Quiz Data")]
    public string jsonFileName = "quiz.json"; // ada di StreamingAssets
    private QuizQuestion[] questions;

    private int currentIndex = 0;
    private int score = 0;

    void Awake()
    {
        StartCoroutine(LoadQuestions());

        if (nextButton != null)
            nextButton.gameObject.SetActive(false);
    }

    IEnumerator LoadQuestions()
    {
        string path = Path.Combine(Application.streamingAssetsPath, jsonFileName);
        string json = "";

        if (path.Contains("://") || path.Contains("jar:")) // Android/WebGL
        {
            using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(path))
            {
                yield return www.SendWebRequest();
                if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                    json = www.downloadHandler.text;
                else
                    Debug.LogError("Gagal load quiz.json: " + www.error);
            }
        }
        else // PC/Mac/Windows
        {
            json = File.ReadAllText(path);
        }

        if (!string.IsNullOrEmpty(json))
        {
            QuizData data = JsonUtility.FromJson<QuizData>(json);
            questions = data.questions;
        }
    }

    public void StartQuiz()
    {
        if (InfoHotspot.IsPopupOpen) return;
        if (questions == null || questions.Length == 0)
        {
            Debug.LogWarning("Soal belum termuat, coba lagi sebentar.");
            return;
        }

        currentIndex = 0;
        score = 0;
        quizPanel.SetActive(true);
        InfoHotspot.IsPopupOpen = true;

        foreach (var btn in optionButtons)
            btn.gameObject.SetActive(true);

        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        ShowQuestion();
    }

    void ShowQuestion()
    {
        feedbackText.text = "";
        var q = questions[currentIndex];
        questionText.text = q.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionTexts[i].text = q.options[i];
            int capturedIndex = i; // penting! hindari closure bug
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnAnswerSelected(capturedIndex));
            optionButtons[i].interactable = true;
        }
    }

    void OnAnswerSelected(int selectedIndex)
    {
        var q = questions[currentIndex];
        bool isCorrect = selectedIndex == q.correctAnswerIndex;

        feedbackText.text = isCorrect ? "Benar!" : "Salah!";
        if (isCorrect) score++;

        scoreText.text = $"Skor: {score}/{questions.Length}";

        foreach (var btn in optionButtons)
            btn.interactable = false;

        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(true);

            // Kalau ini soal terakhir, ubah teks tombol jadi "Selesai"
            bool isLastQuestion = currentIndex == questions.Length - 1;
            if (nextButtonText != null)
                nextButtonText.text = isLastQuestion ? "Selesai" : "Lanjut";
        }
    }

    // Dipanggil dari OnClick() tombol Next
    public void NextQuestion()
    {
        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        currentIndex++;
        if (currentIndex < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        questionText.text = "Kuis selesai!";
        feedbackText.text = $"Skor akhir kamu: {score}/{questions.Length}";

        foreach (var btn in optionButtons)
            btn.gameObject.SetActive(false);
    }

    public void HideQuiz()
    {
        quizPanel.SetActive(false);
        InfoHotspot.IsPopupOpen = false;

        foreach (var btn in optionButtons)
            btn.gameObject.SetActive(true);

        if (nextButton != null)
            nextButton.gameObject.SetActive(false);
    }
}