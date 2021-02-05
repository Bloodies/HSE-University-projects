package com.example.company.myapplication

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import android.app.NotificationManager
import android.app.PendingIntent
import android.content.Context
import android.content.Intent
import androidx.core.app.NotificationCompat
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val intent = Intent(this,MainActivity::class.java)
        val resultIntent = PendingIntent.getActivity(this,0,intent,PendingIntent.FLAG_UPDATE_CURRENT)
        val builder = NotificationCompat.Builder(this)
                .setSmallIcon(android.R.drawable.alert_dark_frame)
                .setContentTitle("Title")
                .setContentText("Text")
                //.setContentIntent(resultIntent)
                //.setAutoCancel(true)
                //.setContentInfo("Info")
                //.setNumber(1)
                //.setWhen(1)
                //.setOngoing(true)
                //.setStyle(NotificationCompat.BigTextStyle().bigText("TEXT"))
                //.setStyle(NotificationCompat.InboxStyle().addLine("Line1"))
        notify.setOnClickListener{
            val notification = builder.build()
            val notificationManager = getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
            notificationManager.notify(1,notification)
            //notificationManager.cancel(1)
            //notificationManager.cancelAll()
        }



    }
}
