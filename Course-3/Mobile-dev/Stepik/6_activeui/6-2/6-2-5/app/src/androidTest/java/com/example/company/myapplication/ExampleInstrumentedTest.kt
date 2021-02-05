package com.example.company.myapplication

import android.app.Activity
import android.support.test.espresso.intent.rule.IntentsTestRule
import android.support.test.rule.ActivityTestRule
import android.support.test.rule.GrantPermissionRule
import org.junit.Rule


import com.example.company.lib.Checker


class Test : Checker() {

    @Rule
    @JvmField
    var mActivityRule = IntentsTestRule(MainActivity::class.java)

    override fun getActivity(): Activity?{
        return mActivityRule.activity
    }
}