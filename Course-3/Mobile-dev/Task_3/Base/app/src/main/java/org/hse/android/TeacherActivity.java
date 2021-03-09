package org.hse.android;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Objects;

public class TeacherActivity extends BaseActivity {

    private static final String TAG = "TeacherActivity";

    private TextView status, subject, cabinet, corp, teacher;
    private Spinner spinner;
    public Date currentTime;

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_teacher);
        Objects.requireNonNull(getSupportActionBar()).hide();

        spinner = findViewById(R.id.groupList);

        List<StudentActivity.Group> groups = new ArrayList<>();
        initGroupList(groups);

        ArrayAdapter<?> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, groups);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        spinner.setAdapter(adapter);

        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> parent, View itemSelected, int selectedItemPosition, long selectedId) {
                Object item = adapter.getItem(selectedItemPosition);
                Log.d(TAG,"selectedItem: " + item);
            }
            public void onNothingSelected(AdapterView<?> parent) { }
        });

        initTime();

        status = findViewById(R.id.status);
        subject = findViewById(R.id.subject);
        cabinet = findViewById(R.id.cabinet);
        corp = findViewById(R.id.corp);
        teacher = findViewById(R.id.teacher);

        View scheduleDay = findViewById(R.id.schedule_day);
        scheduleDay.setOnClickListener(v -> showSchedule(ScheduleType.DAY));
        View scheduleWeek = findViewById(R.id.schedule_week);
        scheduleWeek.setOnClickListener(v -> showSchedule(ScheduleType.WEEK));
    }

    private void initGroupList(List<StudentActivity.Group> groups){
        groups.add(new StudentActivity.Group(1,"Преподователь 1"));
        groups.add(new StudentActivity.Group(2,"Преподователь 2"));
    }

    private void showSchedule(ScheduleType type) {
        Object selectedItem = spinner.getSelectedItem();
        if (!(selectedItem instanceof StudentActivity.Group)) { return; }
        showScheduleImpl(type, (StudentActivity.Group) selectedItem, currentTime);
    }

    protected void showScheduleImpl(ScheduleType type, StudentActivity.Group group, Date currentTime) {
        Intent intent = new Intent(this, ScheduleActivity.class);
        intent.putExtra(ScheduleActivity.ARG_NAME, group.getName());
        intent.putExtra(ScheduleActivity.ARG_ID, group.getId());
        intent.putExtra(ScheduleActivity.ARG_TYPE, type);
        intent.putExtra(ScheduleActivity.ARG_MODE, ScheduleMode.TEACHER);
        intent.putExtra(ScheduleActivity.ARG_TIME, currentTime);
        startActivity(intent);
    }
}