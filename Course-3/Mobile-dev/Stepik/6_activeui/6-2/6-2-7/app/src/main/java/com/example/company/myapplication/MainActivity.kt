package com.example.company.myapplication

import android.os.Bundle
import android.support.v7.app.AppCompatActivity
import android.view.View
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.SeekBar
import android.widget.TextView
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val dataArray = mutableListOf<Int>()
        var adapter = ArrayAdapter<Int>(this, android.R.layout.simple_list_item_1, dataArray)
        listView.adapter = adapter

        seekBar.setOnSeekBarChangeListener(object: SeekBar.OnSeekBarChangeListener {
            override fun onProgressChanged(p0: SeekBar?, p1: Int, p2: Boolean) { }
            override fun onStartTrackingTouch(p0: SeekBar?) {
                dataArray.clear()
                adapter.notifyDataSetChanged()
            }
            override fun onStopTrackingTouch(p0: SeekBar?) {
                if (seekBar.progress == 0) {
                    dataArray.add(0)
                    adapter.notifyDataSetChanged()
                }
                else {
                    for (i in 0 until seekBar.progress) {
                        dataArray.add(i * i)
                        adapter.notifyDataSetChanged()
                    }
                }
            }
        })
    }
}