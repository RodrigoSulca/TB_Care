package com.example.datepicker;

import android.app.DatePickerDialog;
import android.app.TimePickerDialog;
import android.app.Activity;
import android.widget.DatePicker;
import java.util.Calendar;
import com.unity3d.player.UnityPlayer;

public class DatePickerPlugin {

    public void showDatePicker(Activity activity) {
        activity.runOnUiThread(() -> {

            Calendar calendar = Calendar.getInstance();

            DatePickerDialog dialog = new DatePickerDialog(activity,
                (view, year, month, day) -> {
                    String date = day + "/" + (month + 1) + "/" + year;
                    UnityPlayer.UnitySendMessage(
                        "MedicineManager",
                        "OnDateSelected",
                        date      
                    );
                },
                calendar.get(Calendar.YEAR),
                calendar.get(Calendar.MONTH),
                calendar.get(Calendar.DAY_OF_MONTH)
            );

            dialog.show();
        });
    }

    public void showTimePicker(Activity activity) {
        activity.runOnUiThread(() -> {

            final Calendar calendar = Calendar.getInstance();

            int hour = calendar.get(Calendar.HOUR_OF_DAY);
            int minute = calendar.get(Calendar.MINUTE);

            TimePickerDialog dialog = new TimePickerDialog(activity,
                (view, selectedHour, selectedMinute) -> {
                    String time = selectedHour + ":" + selectedMinute;

                    UnityPlayer.UnitySendMessage(
                        "MedicineManager",      // mismo GameObject
                        "OnTimeSelected",   // método en Unity
                        time
                    );
                },
                hour, minute, true // true = formato 24h
            );

            dialog.show();
        });
    }
}