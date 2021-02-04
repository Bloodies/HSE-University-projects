package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import kotlinx.android.synthetic.main.activity_main.*

fun isPrime(arg:Int): Boolean {
    var res:Boolean = true
    for(i in 2..(arg/2)){
        if(arg % i == 0){ res = false }
    }
    if(arg < 2){ res = false }
    return res
}

class MainActivity : AppCompatActivity() {

    fun handler0(v : View){
        if(editText.text.toString().toIntOrNull() == null) {
            textView.setText("error")
        }
        else if(isPrime(editText.text.toString().toInt())){
            textView.setText("prime")
        }
        else { textView.setText("not prime") }
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        checkBtn.setOnClickListener(this::handler0)
    }
}