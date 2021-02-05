package com.example.company.myapplication

import android.app.Activity
import android.support.test.rule.ActivityTestRule
import android.support.test.rule.GrantPermissionRule
import org.junit.Rule


import com.example.company.lib.Checker


class Test : Checker() {

    @Rule
    @JvmField
    var mActivityRule = ActivityTestRule(MainActivity::class.java)
    @get:Rule var permissionWRule = GrantPermissionRule.grant(android.Manifest.permission.WRITE_EXTERNAL_STORAGE)
    @get:Rule var permissionRRule = GrantPermissionRule.grant(android.Manifest.permission.READ_EXTERNAL_STORAGE)


    override fun getActivity(): Activity?{
        return mActivityRule.activity
    }
}