package com.example.company.myapplication

import android.os.Bundle
import android.support.v7.app.AppCompatActivity
import android.text.Editable
import android.text.TextWatcher
import android.widget.ArrayAdapter
import android.widget.Spinner
import android.widget.TextView
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val dataArray = MutableList<String>(1, { x -> "${0}" })
        var adapter = ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, dataArray)
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        spinner.adapter = adapter

        editText.addTextChangedListener(object : TextWatcher {
            override fun afterTextChanged(s: Editable?) {
                try {
                    dataArray.clear()
                    var chislo: Int = s.toString().toInt()

                    if ((chislo > 10)&&(chislo <= 20)) chislo -= 10
                    if (chislo > 100) chislo -= 100
                    for (i in 0 until chislo) {
                        dataArray.add("${i+1}")
                    }
                    adapter.notifyDataSetChanged()
                } catch (E: Exception) {
                    dataArray.clear()
                    adapter.notifyDataSetChanged()
                    return
                }
            }
            override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) { }
            override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) { }
        })
    }
}