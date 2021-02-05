package com.example.company.myapplication

import android.os.Bundle
import android.os.Environment
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.activity_main.*
import java.io.File
import java.nio.file.FileSystem

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        perform.setOnClickListener {
            var sum = 0
            var p :String = path.text.toString()
            try {

                p = p.replace("\n","").replace(" ","").trim()
                val tost = Toast.makeText(this,"$p", Toast.LENGTH_LONG)
                tost.show()
                var my = File( "$p")

                if (my.isFile) {
                    result.text="Error"
                } else {
                    val array = my.listFiles()

                    for (i in array) {
                        if (i.isFile) sum += 1
                    }
                    result.text = sum.toString()

                }
            } catch (E:NoSuchFileException) {result.text="Error"}
        }
    }
}