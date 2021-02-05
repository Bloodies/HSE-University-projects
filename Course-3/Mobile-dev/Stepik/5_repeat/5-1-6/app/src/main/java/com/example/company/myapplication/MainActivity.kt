package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    fun handler(v : View){
        var s:String = ""
        if(rbSphere.isChecked){
            tvAnswer.setText("Правильно!")
        } else if(rgAnswers.checkedRadioButtonId != -1 ){
            tvAnswer.setText("Неправильно!${s}")
        } else {
            tvAnswer.setText("Неправильно!")
        }


    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        butAnswer.setOnClickListener {
                if(rbSphere.isChecked){
                    tvAnswer.setText("Правильно!")
                } else if(rgAnswers.checkedRadioButtonId != -1 ){
                    tvAnswer.setText("Неправильно!")
                } else {
                    tvAnswer.setText("Неправильно!")
                }
        }
    }
}