package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.os.Environment
import kotlinx.android.synthetic.main.activity_main.*
import java.io.File


class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        perform.setOnClickListener {
            var contents: String = ""
            var contents2: String = ""

            if ((filepath.text.toString() != "") || (filepath2.text.toString() != "")) {
                val f1 = File(Environment.getExternalStorageDirectory().toString()+"/${filepath.text.toString()}")
                if (f1.exists()) { contents = f1.readText() }
                else { filepath.setText("error") }

                val f2 = File(Environment.getExternalStorageDirectory().toString()+"/${filepath2.text.toString()}")
                if (f2.exists()) { contents2 = f2.readText() }
                else { filepath2.setText("error") }

                if (filepath.text.toString() != "error" && filepath2.text.toString() != "error") {
                    f1.writeText(contents2)
                    f2.writeText(contents)
                }
            }
        }
    }
}
