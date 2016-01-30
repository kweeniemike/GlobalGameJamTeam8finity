using UnityEngine;
using System.Collections;

public class Date : MonoBehaviour {
    public string PrevDate;
    public string ThisDate;
    public UnityEngine.UI.Text TextPrevDate;
    public UnityEngine.UI.Text TextThisDate;
    public UnityEngine.UI.Image BGImage;
    public Material Eyematerial;
    public bool FromFaliureState = false;
    public int HeightOffset = 0;

    private Vector3 targetPosition = new Vector3(0, 0, 0);
    public float speed = 10.0f;
    public float threshold = 0.5f;
    private float starttime = 0f;
    public float StartTimeDelay = 2.0f;
    public float midtime = 0f;
    public float fadeSpeed = 1.0f;
    private bool fadeout = false;
    private bool TextFadeoutComplete = false;

    void Update()
    {
        if (TextFadeoutComplete && midtime + StartTimeDelay < Time.time)
        {
            //Debug.LogError("hello");
            TextThisDate.color = Color.clear;
            TextPrevDate.color = Color.clear;
            BGImage.color = Color.clear;
        }
        if (!FromFaliureState && starttime + StartTimeDelay < Time.time)
        {
            Vector3 direction = targetPosition - TextPrevDate.rectTransform.position;
            if (direction.magnitude > threshold)
            {
                direction.Normalize();
                TextPrevDate.rectTransform.position = TextPrevDate.rectTransform.position + direction * speed * Time.deltaTime;
                TextPrevDate.color = Color.Lerp(TextPrevDate.color, Color.clear, fadeSpeed * Time.deltaTime);
                midtime = Time.time;
            }
            else {
                TextPrevDate.rectTransform.position = targetPosition;
                TextThisDate.text = ThisDate;
                //TextThisDate.color = Color.Lerp(Color.red, Color.white, fadeSpeed * Time.deltaTime);
                fadeout = true;
            }
            if (fadeout && midtime + StartTimeDelay < Time.time)
            {
                TextThisDate.color = Color.Lerp(TextThisDate.color, Color.clear, fadeSpeed * Time.deltaTime);
                if (fadeSpeed * Time.deltaTime < 0.1)
                {
                    TextFadeoutComplete = true;
                    midtime = Time.time;
                }
            }
        }
        else if (FromFaliureState && starttime + StartTimeDelay < Time.time)
        {
            TextThisDate.text = ThisDate;
            TextThisDate.color = Color.Lerp(TextThisDate.color, Color.clear, fadeSpeed * Time.deltaTime);
            if (fadeSpeed * Time.deltaTime < 0.1)
            {
                TextFadeoutComplete = true;
                midtime = Time.time;
            }
                
        }
         
    }

    void Start () {
        

    }

    void OnEnable()
    {
        int width = Screen.width;
        int height = Screen.height;
        
        TextPrevDate.rectTransform.transform.Translate(width/2, height/2, 0);
        TextThisDate.rectTransform.transform.Translate(width / 2, height / 2, 0);
        
        if (FromFaliureState)
        {
            TextPrevDate.text = "";
            TextThisDate.text = ThisDate;
        }
        else
        {
            TextPrevDate.text = PrevDate;
            TextThisDate.text = "";
        }

        targetPosition = new Vector3(width/2, (height/2) + HeightOffset, 0);

        starttime = Time.time;
    }
}
