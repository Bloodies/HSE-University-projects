package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.os.Environment
import android.support.v4.app.ActivityCompat
import android.support.v4.content.ContextCompat
import kotlinx.android.synthetic.main.activity_main.*
import java.io.*


class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        perform.setOnClickListener {
            val filename = filepath.text.toString()
            if (filename != null && filename.trim() != "") {
                val file = File(Environment.getExternalStorageDirectory().toString()+"/$filename")
                val contents = file.readText()
                result.text = contents
            }
        }
      }
}
