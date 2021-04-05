package org.hse.android.database;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.Query;
import androidx.room.Transaction;
import org.hse.android.entities.GroupEntity;
import org.hse.android.entities.TeacherEntity;
import org.hse.android.entities.TimeTableEntity;
import org.hse.android.entities.TimeTableWithTeacherEntity;
import java.util.Date;
import java.util.List;

@Dao
public interface HseDao {
    @Query("SELECT * FROM `group`")
    LiveData<List<GroupEntity>> getAllGroup();

    @Insert
    void insertGroup(List<GroupEntity> data);

    @Delete
    void delete(GroupEntity data);

    @Query("SELECT * FROM `teacher`")
    LiveData<List<TeacherEntity>> getAllTeacher();

    @Insert
    void insertTeacher(List<TeacherEntity> data);

    @Delete
    void delete(TeacherEntity data);

    @Query("SELECT * FROM time_table")
    LiveData<List<TimeTableEntity>> getAllTimeTable();

    @Query("SELECT * FROM `time_table`")
    LiveData<List<TimeTableWithTeacherEntity>> getTimeTableTeacher();

    @Insert
    void insertTimeTable(List<TimeTableEntity> data);

    @Transaction
    @Query("SELECT * FROM time_table " +
            "  where teacher_id = :teacherId " +
            "  and :date between time_start and time_end")
    LiveData<TimeTableWithTeacherEntity> getTimeTableTeacher(Date date, int teacherId);

    @Transaction
    @Query("SELECT * FROM time_table " +
            "  where group_id = :groupId " +
            "  and :date between time_start and time_end")
    LiveData<TimeTableWithTeacherEntity> getTimeTableGroup(Date date, int groupId);

    @Transaction
    @Query("SELECT * FROM time_table " +
            "  where teacher_id = :teacherId " +
            "  and :start < time_end" +
            "  and :end > time_start")
    LiveData<List<TimeTableWithTeacherEntity>> getTimeTableTeacherRange(Date start, Date end, int teacherId);

    @Transaction
    @Query("SELECT * FROM time_table " +
            "  where group_id = :groupId " +
            "  and :start < time_end" +
            "  and :end > time_start")
    LiveData<List<TimeTableWithTeacherEntity>> getTimeTableGroupRange(Date start, Date end, int groupId);
}