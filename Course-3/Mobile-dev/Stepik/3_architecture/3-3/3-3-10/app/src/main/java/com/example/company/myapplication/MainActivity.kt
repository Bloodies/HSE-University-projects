package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import com.example.company.lib.Log

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        // Write your code here

        Log.d("DEBUG","DEBUG")
        Log.w ("WARNING","WARNING")
        Log.e("ERROR","ERROR")
        Log.i("INFO","INFO")
        Log.printAnswer()


    }
}
