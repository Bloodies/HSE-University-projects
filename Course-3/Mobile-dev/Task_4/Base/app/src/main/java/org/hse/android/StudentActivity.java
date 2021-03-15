package org.hse.android;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;

import androidx.lifecycle.ViewModelProviders;

import org.hse.android.database.GroupEntity;
import org.hse.android.database.MainViewModel;
import org.hse.android.database.TimeTableEntity;
import org.hse.android.database.TimeTableWithTeacherEntity;
import org.jetbrains.annotations.Nullable;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Objects;

public class StudentActivity extends BaseActivity {
    protected MainViewModel mainViewModel;

    private static final String TAG = "StudentActivity";

    private TextView status, subject, cabinet, corp, teacher;
    private Spinner spinner;
    public Date currentTime;

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_student);
        Objects.requireNonNull(getSupportActionBar()).hide();
        mainViewModel = ViewModelProviders.of(this).get(MainViewModel.class);

        spinner = findViewById(R.id.groupList);

        List<Group> groups = new ArrayList<>();
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

    private void initData() { initDataFromTimeTable(null); }

    @Override
    public void showTime(Date dateTime) {
        super.showTime(dateTime);
        mainViewModel.getTimeTableTeacherByDate(dateTime).observe(this, new Observer<List<TimeTableWithTeacherEntity>>() {
            @Override public void onChanged(@Nullable List<TimeTableWithTeacherEntity> list) {
                for (TimeTableWithTeacherEntity listEntity : list) {
                    Log.d(TAG, listEntity.timeTableEntity.subjName + " " + listEntity.teacherEntity.fio);
                    // TODO move to DB query
                    if (getSelectedGroup() != null && getSelectedGroup().getId().equals(listEntity.timeTableEntity.groupId)) {
                        initDataFromTimeTable(listEntity);
                    }
                }
            }
        });
    }

    private void initGroupList(final List<Group> groups){
        mainViewModel.getGroups().observe(this, new Observer<List<GroupEntity>>() {
            @Override public void onChanged(@Nullable List<GroupEntity> list) {
                List<Group> groupsResult = new ArrayList<>();
                for (GroupEntity listEntity : list) {
                    groupsResult.add(new Group(listEntity.id, listEntity.name));
                }
                adapter.clear();
                adapter.addAll(groupsResult);
            }
        });
    }

    //private void initGroupList(List<Group> groups){
    //    String[] pr = { "ПИ", "БИ", "УБ", "Э", "И", "Ю" };
    //    String[] yr = { "16", "17", "18", "19", "20" };
    //    int i=0;
    //    for (String p : pr) {
    //        for (String y : yr) {
    //            for (int z = 1; z < 5; z++) {
    //                i++;
    //                groups.add(new Group(i, p + "-" + y + "-" + z));
    //            }
    //        }
    //    }
    //}

    private void showSchedule(ScheduleType type) {
        Object selectedItem = spinner.getSelectedItem();
        if (!(selectedItem instanceof Group)) { return; }
        showScheduleImpl(type, (Group) selectedItem, currentTime);
    }

    protected void showScheduleImpl(ScheduleType type, Group group, Date currentTime) {
        Intent intent = new Intent(this, ScheduleActivity.class);
        intent.putExtra(ScheduleActivity.ARG_NAME, group.getName());
        intent.putExtra(ScheduleActivity.ARG_ID, group.getId());
        intent.putExtra(ScheduleActivity.ARG_TYPE, type);
        intent.putExtra(ScheduleActivity.ARG_MODE, ScheduleMode.STUDENT);
        intent.putExtra(ScheduleActivity.ARG_TIME, currentTime);
        startActivity(intent);
    }

    private void initDataFromTimeTable(TimeTableWithTeacherEntity timeTableTeacherEntity) {
        if (timeTableTeacherEntity == null) {
            status.setText("Нет пар");

            subject.setText("Дисциплина");
            cabinet.setText("Кабинет");
            corp.setText("Корпус");
            teacher.setText("Преподаватель");
            return;
        }
        status.setText("Идет пара");
        TimeTableEntity timeTableEntity = timeTableTeacherEntity.timeTableEntity;

        subject.setText(timeTableEntity.subjName);
        cabinet.setText(timeTableEntity.cabinet);
        corp.setText(timeTableEntity.corp);
        teacher.setText(timeTableTeacherEntity.teacherEntity.fio);
    }

    static class Group{
        private Integer id;
        private String name;

        public Group(Integer id, String name){
            this.id = id;
            this.name = name;
        }
        @Override public String toString() { return name; }

        public Integer getId(){ return id; }
        public void setId(Integer id) { this.id = id; }
        public String getName() { return name; }
        public void setName(String name){ this.name = name; }
        public Integer getSelectedGroup() {
            return id;
        }
    }
}