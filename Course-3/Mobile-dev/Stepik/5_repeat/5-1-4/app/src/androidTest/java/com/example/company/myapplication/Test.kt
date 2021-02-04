package com.example.company.myapplication

import android.app.Activity
import android.support.test.rule.ActivityTestRule
import com.example.company.lib.Checker
import org.junit.Rule

class Test : Checker() {

    @Rule
    @JvmField
    var mActivityRule = ActivityTestRule(MainActivity::class.java)

    override fun getActivity(): Activity? {
        return mActivityRule.activity
    }
}

