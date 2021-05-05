# cs_mariadb_connection
Mariadb CRUD 및 간단한 Winform 적용 예 

## Nuget 추가

### Mysql.Data 설치
----------
## Programing

### Connection

connection 정보를 String 변수로 선언하여 연결한다.
connection.Open() 으로 연결

```
public Boolean ConnectionDB()
        {
            if (IsConnection())
            {
                return true;
            }
            try
            {
                connectionString =
		 "server=" + server + ";port=" + port + ";user=" + user + ";database=" + database + ";password=" + password;
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
            
        }

```

### quary1(insert, delete, alter...)
update row 개수 결과값을 받음
 
```
public Boolean CommandDB(String sql) 
        {         
            try
            {
                if (ConnectionDB())
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(sql, connection);
                    if (mySqlCommand.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }
                    disConnection();
                }         
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return false;
        }
```

### quary2(select...)
결과 row들을  MySqlDataReader로 읽은 후 DataTable객체에 Load.
 
```
public DataTable SelectDB(String sql)
        {
            var dataTable = new DataTable();
            try
            {
                if (ConnectionDB())
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(sql, connection);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                    dataTable.Load(mySqlDataReader);
                    disConnection();
                }
            } catch (Exception e) 
            {
                MessageBox.Show(e.ToString());
            }
            return dataTable;
        }
```


### Winform example
Mariadb wrapper class 생성 후 winform 에서 쿼리 후 결과 확인
dataGridView에 바인딩
```
private void btn_select_Click(object sender, EventArgs e)
        {
            string quary = txt_select.Text;
            DataTable dt = mariadb.SelectDB(quary);
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
                lbl_result.Text = "select complete";
            }
        }
```
