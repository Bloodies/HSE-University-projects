package com.example.company.myapplication

import android.app.NotificationChannel
import android.app.NotificationManager
import android.os.Build
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.NotificationCompat
import androidx.core.app.NotificationManagerCompat
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    companion object {
        const val notificationId = 101
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        notify.setOnClickListener {
            // Создаём канал

            if (Build.VERSION.SDK_INT >= 26) {
                val channel = NotificationChannel("Cat channel", "channel", NotificationManager
                        .IMPORTANCE_DEFAULT).apply {
                    //description = "Feed cat"
                    setShowBadge(true)
                }
                // Создаем уведомление
                val builder = NotificationCompat.Builder(this, "Cat channel")
                        .setSmallIcon(R.drawable.ic_launcher_foreground)
                        .setContentTitle("Title")
                        .setContentText(editText.text.toString())
                //.setAutoCancel(true)
                //.setPriority(NotificationCompat.PRIORITY_DEFAULT)

                with(NotificationManagerCompat.from(this)) {
                    //createNotificationChannel(channel)  // регистрируем канал
                    notify(notificationId, builder.build()) // посылаем уведомление
                }
            }

        }

    }
}