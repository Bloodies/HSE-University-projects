package com.example.company.myapplication

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.AdapterView
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity(), AdapterView.OnItemSelectedListener {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        spinner11.onItemSelectedListener = this
        spinner12.onItemSelectedListener = this
        spinner13.onItemSelectedListener = this
        spinner21.onItemSelectedListener = this
        spinner22.onItemSelectedListener = this
        spinner23.onItemSelectedListener = this
        spinner31.onItemSelectedListener = this
        spinner32.onItemSelectedListener = this
        spinner33.onItemSelectedListener = this
    }

    override fun onItemSelected(arg0: AdapterView<*>, arg1: View, position: Int, id: Long) {
        var allSelected : Boolean = false
        var oWon: Boolean = false
        var xWon: Boolean = false
        var target: Int = 0
        var oCnt: Int = 0
        var xCnt: Int = 0
        if(!(spinner11.selectedItemId.toInt() == 0 || spinner12.selectedItemId.toInt() == 0 || spinner13.selectedItemId.toInt() == 0
                        || spinner21.selectedItemId.toInt() == 0 || spinner22.selectedItemId.toInt() == 0 || spinner23.selectedItemId.toInt() == 0
                        || spinner31.selectedItemId.toInt() == 0 || spinner32.selectedItemId.toInt() == 0 || spinner33.selectedItemId.toInt() == 0))
                            allSelected = true
        target = 1

        if((spinner11.selectedItemId.toInt() == target && spinner12.selectedItemId.toInt() == target && spinner13.selectedItemId.toInt() == target)
                || (spinner21.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner23.selectedItemId.toInt() == target)
                || (spinner31.selectedItemId.toInt() == target && spinner32.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)

                || (spinner11.selectedItemId.toInt() == target && spinner21.selectedItemId.toInt() == target && spinner31.selectedItemId.toInt() == target)
                || (spinner12.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner32.selectedItemId.toInt() == target)
                || (spinner13.selectedItemId.toInt() == target && spinner23.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)

                || (spinner11.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)
                || (spinner13.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner31.selectedItemId.toInt() == target))
                    oWon = true
        target = 2

        if((spinner11.selectedItemId.toInt() == target && spinner12.selectedItemId.toInt() == target && spinner13.selectedItemId.toInt() == target)
                || (spinner21.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner23.selectedItemId.toInt() == target)
                || (spinner31.selectedItemId.toInt() == target && spinner32.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)

                || (spinner11.selectedItemId.toInt() == target && spinner21.selectedItemId.toInt() == target && spinner31.selectedItemId.toInt() == target)
                || (spinner12.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner32.selectedItemId.toInt() == target)
                || (spinner13.selectedItemId.toInt() == target && spinner23.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)

                || (spinner11.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)
                || (spinner13.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner31.selectedItemId.toInt() == target))
                    xWon = true

        if(spinner11.selectedItemId.toInt() == 1) oCnt++
        if(spinner12.selectedItemId.toInt() == 1) oCnt++
        if(spinner13.selectedItemId.toInt() == 1) oCnt++

        if(spinner21.selectedItemId.toInt() == 1) oCnt++
        if(spinner22.selectedItemId.toInt() == 1) oCnt++
        if(spinner23.selectedItemId.toInt() == 1) oCnt++

        if(spinner31.selectedItemId.toInt() == 1) oCnt++
        if(spinner32.selectedItemId.toInt() == 1) oCnt++
        if(spinner33.selectedItemId.toInt() == 1) oCnt++

        if(spinner11.selectedItemId.toInt() == 2) xCnt++
        if(spinner12.selectedItemId.toInt() == 2) xCnt++
        if(spinner13.selectedItemId.toInt() == 2) xCnt++

        if(spinner21.selectedItemId.toInt() == 2) xCnt++
        if(spinner22.selectedItemId.toInt() == 2) xCnt++
        if(spinner23.selectedItemId.toInt() == 2) xCnt++

        if(spinner31.selectedItemId.toInt() == 2) xCnt++
        if(spinner32.selectedItemId.toInt() == 2) xCnt++
        if(spinner33.selectedItemId.toInt() == 2) xCnt++

        if(Math.abs(xCnt-oCnt) > 1) status.setText("Invalid")
        else if(!allSelected) status.setText("")
        else if((xWon && oWon) || (!xWon && !oWon)) status.setText("Draw")
        else if(oWon) status.setText("0 won")
        else  status.setText("X won")
    }

    override fun onNothingSelected(arg0: AdapterView<*>)
    {
        var allSelected : Boolean = false
        var oWon: Boolean = false
        var xWon: Boolean = false
        var target: Int = 0
        var oCnt: Int = 0
        var xCnt: Int = 0
        if(!(spinner11.selectedItemId.toInt() == 0 || spinner12.selectedItemId.toInt() == 0 || spinner13.selectedItemId.toInt() == 0
                        || spinner21.selectedItemId.toInt() == 0 || spinner22.selectedItemId.toInt() == 0 || spinner23.selectedItemId.toInt() == 0
                        || spinner31.selectedItemId.toInt() == 0 || spinner32.selectedItemId.toInt() == 0 || spinner33.selectedItemId.toInt() == 0))
                            allSelected = true
        target = 1

        if((spinner11.selectedItemId.toInt() == target && spinner12.selectedItemId.toInt() == target && spinner13.selectedItemId.toInt() == target)
                || (spinner21.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner23.selectedItemId.toInt() == target)
                || (spinner31.selectedItemId.toInt() == target && spinner32.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)

                || (spinner11.selectedItemId.toInt() == target && spinner21.selectedItemId.toInt() == target && spinner31.selectedItemId.toInt() == target)
                || (spinner12.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner32.selectedItemId.toInt() == target)
                || (spinner13.selectedItemId.toInt() == target && spinner23.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)

                || (spinner11.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)
                || (spinner13.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner31.selectedItemId.toInt() == target))
                    oWon = true
        target = 2

        if((spinner11.selectedItemId.toInt() == target && spinner12.selectedItemId.toInt() == target && spinner13.selectedItemId.toInt() == target)
                || (spinner21.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner23.selectedItemId.toInt() == target)
                || (spinner31.selectedItemId.toInt() == target && spinner32.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)

                || (spinner11.selectedItemId.toInt() == target && spinner21.selectedItemId.toInt() == target && spinner31.selectedItemId.toInt() == target)
                || (spinner12.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner32.selectedItemId.toInt() == target)
                || (spinner13.selectedItemId.toInt() == target && spinner23.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)

                || (spinner11.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner33.selectedItemId.toInt() == target)
                || (spinner13.selectedItemId.toInt() == target && spinner22.selectedItemId.toInt() == target && spinner31.selectedItemId.toInt() == target))
                    xWon = true

        if(spinner11.selectedItemId.toInt() == 1) oCnt++
        if(spinner12.selectedItemId.toInt() == 1) oCnt++
        if(spinner13.selectedItemId.toInt() == 1) oCnt++

        if(spinner21.selectedItemId.toInt() == 1) oCnt++
        if(spinner22.selectedItemId.toInt() == 1) oCnt++
        if(spinner23.selectedItemId.toInt() == 1) oCnt++

        if(spinner31.selectedItemId.toInt() == 1) oCnt++
        if(spinner32.selectedItemId.toInt() == 1) oCnt++
        if(spinner33.selectedItemId.toInt() == 1) oCnt++

        if(spinner11.selectedItemId.toInt() == 2) xCnt++
        if(spinner12.selectedItemId.toInt() == 2) xCnt++
        if(spinner13.selectedItemId.toInt() == 2) xCnt++

        if(spinner21.selectedItemId.toInt() == 2) xCnt++
        if(spinner22.selectedItemId.toInt() == 2) xCnt++
        if(spinner23.selectedItemId.toInt() == 2) xCnt++

        if(spinner31.selectedItemId.toInt() == 2) xCnt++
        if(spinner32.selectedItemId.toInt() == 2) xCnt++
        if(spinner33.selectedItemId.toInt() == 2) xCnt++

        if(Math.abs(xCnt-oCnt) > 1) status.setText("Invalid")
        else if(!allSelected) status.setText("")
        else if((xWon && oWon) || (!xWon && !oWon)) status.setText("Draw")
        else if(oWon) status.setText("0 won")
        else  status.setText("X won")
    }
}