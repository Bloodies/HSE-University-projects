package com.example.company.myapplication

import android.content.Intent
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import kotlinx.android.synthetic.main.activity_main.*


class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        button.setOnClickListener{
            val myIntent = Intent(this, SecondActivity::class.java)
            myIntent.putExtra("info", editText.text.toString())
            startActivity(myIntent)
        }
        editText.setText("")
        textView.text = intent.getStringExtra("info")
    }

    override fun onResume() {
        super.onResume()
        editText.setText("")
        textView.text = intent.getStringExtra("info")
    }
}