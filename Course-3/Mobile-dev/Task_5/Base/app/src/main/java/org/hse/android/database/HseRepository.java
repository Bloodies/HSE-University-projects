package org.hse.android.database;

import android.content.Context;
import androidx.lifecycle.LiveData;
import org.hse.android.entities.GroupEntity;
import org.hse.android.entities.TeacherEntity;
import org.hse.android.entities.TimeTableWithTeacherEntity;
import java.util.Date;
import java.util.List;

public class HseRepository {
    private DatabaseManager databaseManager;
    private HseDao dao;

    public HseRepository(Context context) {
        databaseManager = DatabaseManager.getInstance(context);
        dao = databaseManager.getHseDao();
    }

    public LiveData<List<GroupEntity>> getGroups() {
        return dao.getAllGroup(); }

    public LiveData<List<TeacherEntity>> getTeachers() {
        return dao.getAllTeacher(); }

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeTableTeacherByDate(Date date) {
        return dao.getTimeTableTeacher(); }

    public LiveData<TimeTableWithTeacherEntity> getTimeWithTeacherByDate (Date date, int id){
        return dao.getTimeTableTeacher(date, id); }

    public LiveData<TimeTableWithTeacherEntity> getTimeWithGroupByDate(Date date, int id){
        return dao.getTimeTableGroup(date, id); }

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeWithTeacherByDateRange(Date start, Date end, int teacherId) {
        return dao.getTimeTableTeacherRange(start, end, teacherId); }

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeWithGroupByDateRange(Date start, Date end, int groupId) {
        return dao.getTimeTableGroupRange(start, end, groupId); }
}