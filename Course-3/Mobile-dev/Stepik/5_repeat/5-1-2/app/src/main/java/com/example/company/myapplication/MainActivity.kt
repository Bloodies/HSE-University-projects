package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        editText.addTextChangedListener(object : TextWatcher{
            override fun afterTextChanged(p0: Editable?) { }
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) { }
            override fun onTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                if(editText.isFocused){
                    if((editText.text.toString().toDoubleOrNull() == null) || (editText.text.toString().toDouble() < 0)){
                        status.setText("error")
                    } else {
                        var inKm:Double = (editText.text.toString().toDouble()) / 39370.0
                        editText2.setText(inKm.toString())
                        status.setText("")
                    }
                }
            }
        })

        editText2.addTextChangedListener(object : TextWatcher{
            override fun afterTextChanged(p0: Editable?) { }
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) { }
            override fun onTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                if(editText2.isFocused){
                    if((editText2.text.toString().toDoubleOrNull() == null) || (editText2.text.toString().toDouble() < 0)){
                        status.setText("error")
                    } else {
                        var inKm:Double = (editText2.text.toString().toDouble()) * 39370
                        editText.setText(inKm.toString())
                        status.setText("")
                    }
                }
            }
        })

    }
}
