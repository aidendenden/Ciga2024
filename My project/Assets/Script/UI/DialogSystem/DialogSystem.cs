using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text textLabel;
    //public Image faceImage;

    public GameObject selectButton;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("头像")]
    //public Sprite face01, face02;

    bool textFinished;
    bool cancelTyping;
    bool oneFinish;


    public static bool isTalk;

    List<string> textList = new List<string>();
    

    // Start is called before the first frame update
    void Awake()
    {
        GetTextFormFile(textFile);
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        textFinished = true;
        StartCoroutine("SetTextUI");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && index == textList.Count && !oneFinish)
        {
            isTalk = false;
            oneFinish = true;
            //selectButton.SetActive(true);
            index = 0;
            return;

        }
        if (Input.anyKeyDown&&!oneFinish)
        {

            if (textFinished && !cancelTyping)
            {
                StartCoroutine("SetTextUI");
            }
            else if (!textFinished && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var Line in lineData)
        {
            textList.Add(Line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";
        // 检查并处理特殊标记
        if (textList[index] == "A")
        {
            Debug.Log("111");
            //faceImage.sprite = face01;
            index++; // 跳过到下一个文本
        }
        else if (textList[index] == "B")
        {
            Debug.Log("222");
            //faceImage.sprite = face02;
            index++; // 跳过到下一个文本
        }

        // 确保不越界
        if (index >= textList.Count)
        {
            yield break;
        }
        
        string fullText = textList[index];
        int charIndex = 0;
        while (!cancelTyping && charIndex < fullText.Length)
        {
            // 检查是否是富文本标签的开始
            if (fullText[charIndex] == '<')
            {
                // 查找富文本标签的结束
                int endIndex = fullText.IndexOf('>', charIndex);
                if (endIndex == -1)
                {
                    endIndex = fullText.Length;
                }
                else
                {
                    // 包括标签结束符号
                    endIndex += 1;
                }

                // 添加整个标签
                textLabel.text += fullText.Substring(charIndex, endIndex - charIndex);
                charIndex = endIndex;
            }
            else
            {
                // 添加单个字符
                textLabel.text += fullText[charIndex];
                charIndex++;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        // 确保显示完整的文本
        textLabel.text = fullText;
        cancelTyping = false;
        textFinished = true;
        index++;
    }

}
