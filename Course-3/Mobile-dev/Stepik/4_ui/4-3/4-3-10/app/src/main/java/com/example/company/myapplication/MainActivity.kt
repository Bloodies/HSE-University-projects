package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.app.Activity;
import android.os.Bundle
import android.widget.Button;

class MainActivity : AppCompatActivity() {



    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val myButton = findViewById (R.id.button) as Button

    }
}
