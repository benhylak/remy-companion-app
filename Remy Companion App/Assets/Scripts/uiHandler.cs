using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Unity.Notifications.Android;

public class uiHandler : MonoBehaviour
{
    //Variables for UI Text Elements
    public Text ulBoiling;
    public Text urBoiling;
    public Text llBoiling;
    public Text lrBoiling;

    public Text ulTemp;
    public Text urTemp;
    public Text llTemp;
    public Text lrTemp;

    public Text ulPotDet;
    public Text urPotDet;
    public Text llPotDet;
    public Text lrPotDet;

    public Text ulOnOff;
    public Text urOnOff;
    public Text llOnOff;
    public Text lrOnOff;

    Burner ulBurner;
    Burner urBurner;
    Burner llBurner;
    Burner lrBurner;


    public void Start()
    {
        var c = new AndroidNotificationChannel("channel_id", "default", "generic", Importance.High);
        AndroidNotificationCenter.RegisterNotificationChannel(c);

    }

    public void Update()
    {
        updateText();
    }

    

    public void sendNotif()
    {

        //Testing Mobile Notif Package here
        var notification = new AndroidNotification();
        notification.Title = "SomeTitle";
        notification.Text = "SomeText";
        notification.FireTime = System.DateTime.Now;
        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }


    public void updateText()
    {
        ReactiveCollection<Burner> burners = DatabaseManager.Instance.getBurners();
        

        if (burners.Count < 4)
        {
            Debug.Log("No burners yet");
            return;
        }

        //Finds and labels burners to be used later for pulling data
        for (int i = 0; i < burners.Count; i++)
        {
            if (burners[i].Position.ToString() == "UPPER_LEFT")
            {
                ulBurner = burners[i];
            }
            else if (burners[i].Position.ToString() == "UPPER_RIGHT")
            {
                urBurner = burners[i];
            }
            else if (burners[i].Position.ToString() == "LOWER_LEFT")
            {
                llBurner = burners[i];
            }
            else if (burners[i].Position.ToString() == "LOWER_RIGHT")
            {
                lrBurner = burners[i];
            }
        }

            ulBoiling.text = "Boiling:" + ulBurner.IsBoiling.ToString(); //+ firebase var
        //Tests notification for boiling
        if (ulBurner.IsBoiling.Value)
        {
            int delay = 0;
            UniLocalNotification.Register(delay, "ulburner is boiling", "title");
        }
            urBoiling.text = "Boiling:" + urBurner.IsBoiling.ToString();
            llBoiling.text = "Boiling:" + llBurner.IsBoiling.ToString();
            lrBoiling.text = "Boiling:" + lrBurner.IsBoiling.ToString();

            ulTemp.text = "Temperature:" + ulBurner.Temperature.ToString();//+ firebase var
            urTemp.text = "Temperature:" + urBurner.Temperature.ToString();
            llTemp.text = "Temperature:" + llBurner.Temperature.ToString();
            lrTemp.text = "Temperature:" + lrBurner.Temperature.ToString();

            ulPotDet.text = "Pot Detected:" + ulBurner.IsPotDetected.ToString(); //+ firebase var
            urPotDet.text = "Pot Detected:" + urBurner.IsPotDetected.ToString();
            llPotDet.text = "Pot Detected:" + llBurner.IsPotDetected.ToString();
            lrPotDet.text = "Pot Detected:" + lrBurner.IsPotDetected.ToString();

            ulOnOff.text = "On:" + ulBurner.IsOn.ToString(); //+ firebase var
            urOnOff.text = "On:" + urBurner.IsOn.ToString();
            llOnOff.text = "On:" + llBurner.IsOn.ToString();
            lrOnOff.text = "On:" + lrBurner.IsOn.ToString();
       
    }

    
}
