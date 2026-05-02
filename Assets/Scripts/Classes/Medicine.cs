using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Medicine
{
    public string name;
    public DateTime startDay;
    public List<ScheduleObj> schedules;
    public int[] weekdays;
}
