package org.hse.android.database;

public class Group {
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