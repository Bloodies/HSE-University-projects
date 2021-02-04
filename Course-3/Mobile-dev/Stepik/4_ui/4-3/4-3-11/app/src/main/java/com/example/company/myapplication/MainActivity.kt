package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button;
import android.widget.TextView;
import android.widget.EditText;

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val new_button =  findViewById(R.id.button) as Button
        val new_view =  findViewById(R.id.textView) as TextView
        val new_edit =  findViewById(R.id.editText) as EditText

    }
}
