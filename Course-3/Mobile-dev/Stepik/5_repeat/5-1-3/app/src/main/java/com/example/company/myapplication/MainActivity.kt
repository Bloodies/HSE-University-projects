package com.example.company.myapplication

import android.os.Bundle
import android.view.View
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    fun handler0(v : View){
        if(number.text.toString().toIntOrNull() == null || systemOfCalculus.text.toString().toIntOrNull() == null
                || systemOfCalculus.text.toString().toInt() < 2 || systemOfCalculus.text.toString().toInt() > 36){
            convertResult.setText("Error")
        } else {
            convertResult.setText(number.text.toString().toBigInteger().toString(systemOfCalculus.text.toString().toInt()))
        }
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        convertButton.setOnClickListener(this::handler0)
    }
}
