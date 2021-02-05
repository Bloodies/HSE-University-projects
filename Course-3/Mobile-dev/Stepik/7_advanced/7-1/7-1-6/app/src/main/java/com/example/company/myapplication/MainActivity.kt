package com.example.company.myapplication

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.activity_main.*
import okhttp3.*
import org.json.JSONObject
import java.io.IOException
import java.net.URLDecoder

class MainActivity : AppCompatActivity() {

    var URL = "https://ya.ru"
    var okHttpClient: OkHttpClient = OkHttpClient()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        var urltext: String = editText.text.toString()
        button.setOnClickListener {
            URL = editText.text.toString()
            if (URL != "") loadRandomFact() else textView.text = "Failed"
        }
    }

    private fun loadRandomFact() {
        runOnUiThread {
            //progressBar.visibility = View.VISIBLE
        }

        try {
            val request: Request = Request.Builder().url(URL).build()
            okHttpClient.newCall(request).enqueue(object: Callback {
                override fun onFailure(call: Call?, e: IOException?) {
                    runOnUiThread{
                        textView.text = "Failed"
                    }
                }

                override fun onResponse(call: Call?, response: Response?) {
                    val json = response?.body()?.string()


                    runOnUiThread {
                        textView.text = "Ok"
                        
                    }
                }
            })
        } catch (E:Exception) {textView.text = "Failed"}

    }
}
