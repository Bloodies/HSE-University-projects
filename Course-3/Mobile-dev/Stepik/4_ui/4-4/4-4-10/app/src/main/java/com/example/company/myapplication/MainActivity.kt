package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        add.setOnClickListener { calc(add) }
        subtr.setOnClickListener { calc(subtr) }
        mul.setOnClickListener { calc(mul) }
        divide.setOnClickListener { calc(divide ) }

    }
    fun calc(v: View){
        try {
            arg1.text.toString().toInt()
            arg2.text.toString().toInt()
        }
        catch (e: NumberFormatException){
            answer.text = "Input Error"
            return
        }

        val a1 = arg1.text.toString().toInt()
        val a2 = arg2.text.toString().toInt()

        if (v.id == R.id.add){
            answer.text = (a1+a2).toString()
        }
        else if (v.id == R.id.subtr){
            answer.text = (a1-a2).toString()
        }
        else if (v.id == R.id.mul){
            answer.text = (a1*a2).toString()
        }
        else if (v.id == R.id.divide){
            if (a2 == 0){
                answer.text = "Div by zero"
                return
            }
            answer.text = (a1/a2).toString()
        }
    }
}
