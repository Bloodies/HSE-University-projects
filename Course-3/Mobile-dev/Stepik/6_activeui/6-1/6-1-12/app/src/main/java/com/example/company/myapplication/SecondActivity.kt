package com.example.company.myapplication

import android.content.Intent
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import kotlinx.android.synthetic.main.activity_main.*

class SecondActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_second)

        button.setOnClickListener{
            val myIntent = Intent(this, MainActivity::class.java)
            myIntent.putExtra("info", intent.getStringExtra("info"))
            startActivity(myIntent)
        }
    }
    override fun onPause(){
        super.onPause()
        val myIntent = Intent(this, MainActivity::class.java)
        myIntent.putExtra("info", intent.getStringExtra("info"))
        startActivity(myIntent)
    }
}