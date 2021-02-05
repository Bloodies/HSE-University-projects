package com.example.company.myapplication

import android.app.NotificationChannel
import android.app.NotificationManager
import android.app.PendingIntent
import android.content.Intent
import android.os.Build
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.NotificationCompat
import androidx.core.app.NotificationManagerCompat
import kotlinx.android.synthetic.main.activity_main.*
import android.content.Context
import androidx.core.app.NotificationCompat.PRIORITY_HIGH

class MainActivity : AppCompatActivity() {

    private val NOTIFY_ID = 1
    private val CHANNEL_ID = "CHANNEL_ID"
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val notificationManager = getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
        send_notification.setOnClickListener {
            val intent = Intent(applicationContext, FinishActivity::class.java)
            intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK or Intent.FLAG_ACTIVITY_NEW_TASK)
            val pendingIntent = PendingIntent.getActivity(applicationContext, 0, intent, PendingIntent.FLAG_UPDATE_CURRENT)
            val notification = NotificationCompat.Builder(applicationContext, CHANNEL_ID)
                    .setAutoCancel(true)
                    .setSmallIcon(android.R.drawable.alert_dark_frame)
                    .setWhen(System.currentTimeMillis())
                    .setContentIntent(pendingIntent)
                    .setContentTitle("Title")
                    .setContentText("Text")
                    .setPriority(PRIORITY_HIGH)

            createChannel(notificationManager)
            notificationManager.notify(NOTIFY_ID, notification.build())
        }
    }

    private fun createChannel(manager: NotificationManager) {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            val channel = NotificationChannel(CHANNEL_ID, CHANNEL_ID, NotificationManager.IMPORTANCE_DEFAULT)
            manager.createNotificationChannel(channel)
        }
    }
}