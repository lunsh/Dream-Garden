using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{

    [SerializeField] public GameObject notificationPane;

    public void ClosePane()
    {
        Destroy(notificationPane);
    }
}
