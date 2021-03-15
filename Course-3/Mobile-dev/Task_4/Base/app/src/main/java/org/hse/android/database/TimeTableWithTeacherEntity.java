package org.hse.android.database;

import androidx.room.Embedded;
import androidx.room.Relation;

public class TimeTableWithTeacherEntity {
    @Embedded
    public TimeTableEntity timeTableEntity;

    @Relation(parentColumn = "teacher_id", entityColumn = "id")
    public TeacherEntity teacherEntity;
}
