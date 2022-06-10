using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionDisplay : MonoBehaviour
{
    [SerializeField] private GameObject collectionDisplayPane;
    [SerializeField] private GameObject pageFlip;
    [SerializeField] private GameObject collectionItems;
    [SerializeField] private Image imageBaby;
    [SerializeField] private Image imageAdult;
    [SerializeField] private TMP_Text collectionName;
    [SerializeField] private TMP_Text collectionRarity;
    [SerializeField] private TMP_Text collectionDescription;
    [SerializeField] private Image imageSeed;
    [SerializeField] private TMP_Text collectionSeedDesc;

    public void showCollection(Seed seedData)
    {
        print("showingCollection");
        collectionDisplayPane.SetActive(true);
        imageBaby.sprite = Resources.Load<Sprite>(seedData.textureName + "-baby");
        imageAdult.sprite = Resources.Load<Sprite>(seedData.textureName + "-adult");
        collectionName.text = seedData.name;
        collectionRarity.text = seedData.rarity;
        collectionDescription.text = seedData.description;
        imageSeed.sprite = Resources.Load<Sprite>(seedData.textureName + "-seed");
        collectionSeedDesc.text = seedData.preDescription;
    }

    public void collectionBack()
    {
        StartCoroutine(CollectionSeedDataAnimation());
    }

    IEnumerator CollectionSeedDataAnimation()
    {
        pageFlip.SetActive(true);
        pageFlip.GetComponent<Animator>().SetTrigger("ReverseFlip");
        yield return new WaitForSeconds(0.33f);
        pageFlip.SetActive(false);
        collectionDisplayPane.SetActive(false);
        collectionItems.SetActive(true);
    }
}
