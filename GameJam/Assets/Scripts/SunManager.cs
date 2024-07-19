using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public enum Daytime
    {
        Day,
        Night
    }

    private Daytime daytime = Daytime.Day;
    
    [SerializeField] private GameObject sunObj;
    [SerializeField] private Light sunIlluminationLight;

    private float timer = 0;
    private float maxDayNightInterval = 10f;

    private Vector3 sunStartPos;
    private Vector3 sunEndPos;

    private Coroutine timerRoutine;
    
    // Start is called before the first frame update
    void Start()
    {
        sunStartPos = sunObj.transform.position;
        sunEndPos = sunObj.transform.position + new Vector3(0f, -9f, 0f);
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartTimer()
    {
        if(timerRoutine!=null)
            StopCoroutine(timerRoutine);
        
        timerRoutine = StartCoroutine(TimerCoroutine());
       
    }

    IEnumerator TimerCoroutine()
    {
        timer = maxDayNightInterval;

        while (true)
        {
            yield return new WaitForSeconds(1f);
            timer--;

            if (timer == 5)
            {
                StartCoroutine(SwitchSun());
            }
            
            if (timer <= 0)
                break;
        }
        
        StartTimer();
    }

    IEnumerator SwitchSun()
    {
        Vector3 start;
        Vector3 end;
       
        if(daytime==Daytime.Day)
        {
            start = sunStartPos;
            end = sunEndPos;
            daytime = Daytime.Night;
        }
        else
        {
            start = sunEndPos;
            end = sunStartPos;
            daytime = Daytime.Day;
        }
        
        float t = 0;
        Vector3 currentPos = Vector3.Slerp(start,end,t);
        sunObj.transform.position = currentPos;
        
        if(daytime==Daytime.Night)
            sunIlluminationLight.color = new Color(1, 1, 0);
        else
            sunIlluminationLight.color = new Color(1, 0, 0);
        
        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime/5; 
           
            if(daytime==Daytime.Night)
                sunIlluminationLight.color = new Color(1, 1-t, 0);
            else
                sunIlluminationLight.color = new Color(1, t, 0);
            currentPos = Vector3.Slerp(start,end,t);
            sunObj.transform.position = currentPos;
        }
        
        t = 1;
        if(daytime==Daytime.Night)
            sunIlluminationLight.color = new Color(1, 1-t, 0);
        else
            sunIlluminationLight.color = new Color(1, t, 0);
        currentPos = Vector3.Slerp(start,end,t);
        sunObj.transform.position = currentPos;
        
    }
}
