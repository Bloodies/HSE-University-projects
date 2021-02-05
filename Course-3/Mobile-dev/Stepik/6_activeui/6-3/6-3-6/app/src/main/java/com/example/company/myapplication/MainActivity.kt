package com.example.company.myapplication

import android.os.Bundle
import android.support.v7.app.AppCompatActivity
import android.view.View
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.TextView
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val dataArray = Array(31, {x -> "$x"} )
        var adapter = ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, dataArray)
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        gridView.numColumns = 4
        gridView.adapter = adapter

        gridView.setOnItemClickListener { parent: AdapterView<*>, view: View, position: Int, id: Long ->
            dataArray.set(position,""+(Math.floor(position/4.0)+1).toInt()+","+((position%4)+1))
            adapter.notifyDataSetChanged()
        }
    }
}
