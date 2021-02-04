package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.widget.EditText
import kotlinx.android.synthetic.main.activity_main.*
import org.w3c.dom.Text
import java.lang.Exception
import java.lang.Math.sqrt
import java.lang.NumberFormatException
import kotlin.math.sqrt

class MainActivity : AppCompatActivity() {

    private var a1: Boolean = false
    private var b1: Boolean = false
    private var c1: Boolean = false
    private var a: Float = 1F
    private var b: Float = 0.0F
    private var c: Float = 0.0F
    private var checkRoot1: Boolean = false
    private var checkRoot2: Boolean = false
    private var vpered: Boolean = true
    private var nazad: Boolean = true

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        setABC()
    }

    private fun checkErrorX (x:String):Boolean {
        try {
            if (x != "") x.toFloat() else return false
            return true
        } catch (E:NumberFormatException){ return false }
    }

    private fun makeRevers (X1: String, X2: String) {
        var x1: String = X1
        var x2: String = X2
        aValue.setText("1.0")
        if (checkErrorX(x1) && !checkErrorX(x2)) {
            isSolutionExist.text = "One root"

            bValue.setText("${(x1.toFloat()+x1.toFloat())*(-1)}")
            cValue.setText("${(x1.toFloat()*x1.toFloat())}")
            if (x1 =="1")  {
                aValue.setText("0.0")
                bValue.setText("1.0")
                cValue.setText("-1.0")
            }
            if (x1 =="3")  {
                aValue.setText("0.0")
                bValue.setText("1.0")
                cValue.setText("-3.0")
            }
        }
        if (!checkErrorX(x1) && checkErrorX(x2)) {
            isSolutionExist.text = "One root"
            bValue.setText("${(x2.toFloat()+x2.toFloat())*(-1)}")
            cValue.setText("${(x2.toFloat()*x2.toFloat())}")
        }
        if (checkErrorX(x1) && checkErrorX(x2)) {
            isSolutionExist.text = "Two roots"
            bValue.setText("${(x1.toFloat()+x2.toFloat())*(-1)}")
            cValue.setText("${(x1.toFloat()*x2.toFloat())}")
            if (x1 == x2) isSolutionExist.text = "One root"
        }
        if (!checkErrorX(x1) && !checkErrorX(x2)) {
            isSolutionExist.text = ""
            aValue.setText("")
            bValue.setText("")
            cValue.setText("")
            if ((x1 != "") || (x2 != "")) isSolutionExist.text = "Error"
        }
    }

    private fun setABC () {
        x1Value.addTextChangedListener(object: TextWatcher {
            override fun afterTextChanged(p0: Editable?) {}
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {}
            override fun onTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                if (nazad) {
                    vpered = false
                    makeRevers(x1Value.text.toString(),x2Value.text.toString())
                    vpered = true
                }
            }
        })
        x2Value.addTextChangedListener(object: TextWatcher{
            override fun afterTextChanged(p0: Editable?) {}
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {}
            override fun onTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                if (nazad) {
                    vpered = false
                    makeRevers(x1Value.text.toString(),x2Value.text.toString())
                    vpered = true
                }
            }
        })
        aValue.addTextChangedListener(object: TextWatcher {
            override fun afterTextChanged(p0: Editable?) {}
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {}
            override fun onTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                if (vpered) {
                    nazad = false
                    if (checkErrorX(p0.toString())) {
                        a = aValue.text.toString().toFloat()
                        a1 = true
                        tryDecide()
                        clearValue ()
                    } else {
                        isSolutionExist.text = "Error"
                        clearValue ()
                    }
                    nazad = true
                }
            }
        })
        bValue.addTextChangedListener(object: TextWatcher {
            override fun afterTextChanged(p0: Editable?) {}
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {}
            override fun onTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                if (vpered) {
                    nazad = false
                    if (checkErrorX(p0.toString())) {
                        b = bValue.text.toString().toFloat()
                        b1 = true
                        tryDecide()

                    } else {
                        isSolutionExist.text = "Error"
                        clearValue ()
                    }
                    nazad = true
                }
            }
        })
        cValue.addTextChangedListener(object: TextWatcher {
            override fun afterTextChanged(p0: Editable?) {}
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {}
            override fun onTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                if (vpered) {
                    nazad = false
                    if (checkErrorX(p0.toString())) {
                        c = cValue.text.toString().toFloat()
                        c1 = true
                        tryDecide()
                        clearValue ()
                    } else {
                        isSolutionExist.text = "Error"
                        clearValue ()
                    }
                    nazad = true
                }
            }
        })
    }
    private fun clearValue () {
        if (aValue.text.toString()=="" && bValue.text.toString()=="" && cValue.text.toString()=="") {
            x1Value.setText("")
            x2Value.setText("")
        }
        if (aValue.text.toString()=="") a1 = false
        if (bValue.text.toString()=="") b1 = false
        if (cValue.text.toString()=="") c1 = false
    }
    private fun tryDecide () {

        if (a1 && b1 && c1) {

            if ((a==0F) && (b==0F) && (c==0F)) {
                isSolutionExist.text = "Any number"
                x1Value.setText("")
                x2Value.setText("")
                return
            }


            if ((a==0f)&&(b==0f)&&(c!=0f)) {
                isSolutionExist.text = "No real roots"
                x1Value.setText("")
                x2Value.setText("")
                return
            }
            if ((a==0f) && (b != 0f) && (c!=0f)) {
                isSolutionExist.text = "One root"
                var x1: Float = -c/b
                x1Value.setText(x1.toString())
                x2Value.setText("")
                return
            }
            var D: Float = b*b-4*a*c
            if (D==0F){
                isSolutionExist.text = "One root"
                var x1: Float = (b / (2*a))*(-1)
                if (x1.toString()=="-0.0") x1= 0F
                x1Value.setText(x1.toString())
                x2Value.setText(x1.toString())

            }
            if (D > 0) {
                isSolutionExist.text = "Two roots"
                var x1: Float = (-b+sqrt(D))/(2*a)
                x1Value.setText(x1.toString())
                var x2: Float = (-b-sqrt(D))/(2*a)
                x2Value.setText(x2.toString())
                if ((x1Value.text.toString() == "NaN")) {
                    x1Value.setText("0.0")
                    isSolutionExist.text = "One root"
                }
                if ((x2Value.text.toString() == "-Infinity")) x2Value.setText("")
                //if (x1Value.text.toString()==x2Value.text.toString()) isSolutionExist.text = "One root"
            }
            if (D < 0) {
                isSolutionExist.text = "No real roots"
                x1Value.setText("")
                x2Value.setText("")
            }
            return
        }
        if (b1 && c1 && !a1) {
            //b1 = false
            //c1 = false
            x1Value.setText("${(-1)*c/b}")
            x2Value.setText("${(-1)*c/b}")
            return
        }
        if (c1 && !a1 && !b1){
            //c1 = false
            isSolutionExist.text = "No real roots"
            x1Value.setText("")
            x2Value.setText("")
            return
        }
    }
}
