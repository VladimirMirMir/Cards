using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;
using UnityEditor;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] CardBase cardBase;
	
	[SerializeField] Text cardTitle; 
	[SerializeField] Text atk; 
	[SerializeField] Text hp; 
	[SerializeField] Text mana;
	[SerializeField] Image art;

    Color originalColor = Color.white;
    Color highlightenedColor = Color.red;

    public void OnPointerDown(PointerEventData eventData)
    {
        //transform.parent.SetParent(GameManager.Instance.pointerTransform);
        //GameManager.Instance.pointerTransform.GetChild(0).position = Vector3.zero;
        //GameManager.Instance.pointerTransform.GetChild(0).GetChild(0).position = Vector3.zero;
        //transform.parent.transform.eulerAngles = Vector3.zero;
        //GameManager.Instance.handTransform.GetComponent<HandController>().RearrangeCards();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(1).GetComponent<Image>().color = highlightenedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(1).GetComponent<Image>().color = originalColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //if (Input.mousePosition.y < 600 && Input.mousePosition.y > 100 && Input.mousePosition.x > 400 && Input.mousePosition.x < Screen.width - 400)
        //{
        //    transform.parent.SetParent(GameManager.Instance.panelTransform);
        //}
        //else
        //{
        //    transform.parent.SetParent(GameManager.Instance.handTransform);
        //    GameManager.Instance.handTransform.GetComponent<HandController>().RearrangeCards();
        //}
    }

    void Awake()
	{
		DisplayCard();
	}
	
	void DisplayCard()
	{
        cardTitle.text = cardBase.CardName;
		atk.text = cardBase.Atk.ToString();
		hp.text = cardBase.HP.ToString();
		mana.text = cardBase.Mana.ToString();
        StartCoroutine(GetArt());
	}
	
	IEnumerator GetArt()
	{
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("http://www.picsum.photos/170", false))
		{
			yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
			{
				Debug.Log(uwr.error);
			}
			else
			{
                var texture = DownloadHandlerTexture.GetContent(uwr);
                if (texture != null)
                {
                    byte[] bytes = texture.EncodeToPNG();
                    //AssetDatabase.CreateAsset(bytes, Application.persistentDataPath + "/Assets/Art/sprite.png");
                    //File.WriteAllBytes(Application.persistentDataPath + "/Assets/Art/sprite.png", bytes);
                    Sprite newSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 170);
                    cardBase.Art = newSprite;
                    art.sprite = newSprite;
                    art.useSpriteMesh = true;
                }
			}
		}
	}

    
}
