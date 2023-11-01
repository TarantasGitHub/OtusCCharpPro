using Npgsql;
using System.Configuration;
using System.Text;

namespace ConsoleApp.DbCode
{
    internal class Class1
    {
        public static void GetVersion()
        {
            var connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            object? version;
            using (var con = new NpgsqlConnection(connection))
            {
                con.Open();

                var sql = "SELECT version()";

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    version = cmd.ExecuteScalar();
                }
            }
            Console.WriteLine($"PostgreSQL version: {version}");
        }

        public static void GetAllTable()
        {
            var connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            var dataCollection = new List<object[]>();
            using (var con = new NpgsqlConnection(connection))
            {
                con.Open();

                var sql = "SELECT * FROM pg_catalog.pg_tables WHERE schemaname = 'public';";

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var dataRow = new object[reader.FieldCount];

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataRow[i] = reader.GetValue(i);
                        }
                        dataCollection.Add(dataRow);
                    }
                }
            }
            string tableName = string.Empty;

            foreach (var row in dataCollection)
            {
                if (tableName != row[1].ToString())
                {
                    tableName = row[1].ToString();
                    Console.WriteLine($"{tableName}");
                }
            }
            Console.WriteLine();
        }

        public static void GetTableData(string tablename)
        {
            var connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            var dataCollection = new List<object[]>();
            List<string> columnName;
            List<int> columnWidth;
            using (var con = new NpgsqlConnection(connection))
            {
                con.Open();

                var sql = string.Format("SELECT * FROM {0};", tablename);

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    var reader = cmd.ExecuteReader();
                    columnName = new List<string>(reader.FieldCount);
                    columnWidth = new List<int>(reader.FieldCount);
                    for (var colIndex = 0; colIndex < reader.FieldCount; colIndex++)
                    {
                        columnName.Add(reader.GetName(colIndex));
                        columnWidth.Add(columnName[colIndex].Length);
                    }
                    while (reader.Read())
                    {
                        var dataRow = new object[reader.FieldCount];

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataRow[i] = reader.GetValue(i);
                            if (columnWidth[i] < dataRow[i].ToString().Length)
                            {
                                columnWidth[i] = dataRow[i].ToString().Length;
                            }
                        }
                        dataCollection.Add(dataRow);
                    }
                }
            }
            string tableName = string.Empty;

            foreach (var row in dataCollection)
            {
                if (tableName != row[1].ToString())
                {
                    tableName = row[1].ToString();
                    Console.WriteLine($"{tableName}");
                }
            }
            StringBuilder sb = new StringBuilder("|");
            StringBuilder sb1 = new StringBuilder("+");
            for (int i = 0; i < columnName.Count(); i++)
            {
                sb.Append(columnName[i]);
                for(int k = 0; k < columnName[i].Length; k++)
                {
                    sb1.Append("-");
                }

                for (int j = 0; j < columnWidth[i] - columnName[i].Length; j++)
                {
                    sb.Append(" ");
                    sb1.Append("-");
                }
                sb.Append("|");
                sb1.Append("+");
            }
            Console.WriteLine(sb);
            Console.WriteLine(sb1);
            foreach (var row in dataCollection)
            {
                sb = new StringBuilder("|");
                for (var col = 0; col < row.Count(); col++)
                {
                    sb.Append(row[col]);
                    for(int i = 0; i < columnWidth[col] - row[col].ToString().Length; i++)
                    {
                        sb.Append(" ");
                    }
                    sb.Append("|");
                }
                Console.WriteLine(sb);
            }
            
        }

        public static async Task AddTables()
        {
            var connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (var con = new NpgsqlConnection(connection))
            {
                con.Open();

                var sql = "CREATE TABLE TestTable (aircraft_code char(3) not null, seat_no varchar(4) not null, fare_conditions varchar(10) not null);";

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    var reader = await cmd.ExecuteNonQueryAsync();
                }
            }

            Console.WriteLine();
        }

        public static async Task AddTableAircrafts()
        {
            // Add aircrafts table
            var sql = @"
DROP TABLE IF EXISTS boarding_passes;
DROP TABLE IF EXISTS ticket_flights;
DROP TABLE IF EXISTS tickets;
DROP TABLE IF EXISTS bookings;
DROP TABLE IF EXISTS flights;
DROP TABLE IF EXISTS airports;
DROP TABLE IF EXISTS seats;
DROP TABLE IF EXISTS aircrafts;
CREATE TABLE aircrafts
(   aircraft_code   char(3) not null,
    model           text    not null,
    range           integer not null,
    CHECK   (   range > 0   ),
    PRIMARY KEY (   aircraft_code)
);";

            await AddTable(sql);

            sql = @"
INSERT INTO aircrafts   (   aircraft_code,  model, range    )
VALUES  ( 'SU9', 'Sukhoi SuperJet-100', 3000),
        ( '773', 'Boeing 777-300', 11100),
        ( '763', 'Boeing 767-300', 7900),
        ( '733', 'Boeing 737-300', 4200),
        ( '320', 'Airbus A320-200', 5700),
        ( '321', 'Airbus A321-200', 5600),
        ( '319', 'Airbus A319-100', 6700),
        ( 'CN1', 'Cessna 208 Caravan', 1200),
        ( 'CN2', 'Bombardier CRJ-200', 2700);";

            await AddTable(sql);

            sql = @"
CREATE TABLE seats
(   aircraft_code   char(3)     not null,
    seat_no         varchar(4)  not null,
    fare_conditions varchar(10) not null,
    CHECK   (   fare_conditions IN
        ( 'Economy', 'Comfort', 'Business' )
    ),
    PRIMARY KEY (   aircraft_code, seat_no ),
    FOREIGN KEY (   aircraft_code   )
        REFERENCES  aircrafts   ( aircraft_code )
        ON DELETE   CASCADE
);";
            await AddTable(sql);

            sql = @"
INSERT INTO seats   (   aircraft_code,  seat_no, fare_conditions    )
VALUES  ( 'SU9', '1A', 'Business'),
        ( 'SU9', '1B', 'Business'),
        ( 'SU9', '10A', 'Economy'),
        ( 'SU9', '10B', 'Economy'),
        ( 'SU9', '10F', 'Economy'),
        ( 'SU9', '20F', 'Economy');";

            await AddTable(sql);

            sql = @"
CREATE TABLE airports
(   airport_code    char(3)     not null,
    airport_name    text        not null,
    city            text        not null,
    longitude       float       not null,
    latitude        float       not null,
    timezone        text        not null,
    PRIMARY KEY (   airport_code   )
);";
            await AddTable(sql);

            sql = @"
CREATE TABLE flights
(   flight_id           serial      not null,
    flight_no           char(6)     not null,
    scheduled_departure timestamptz not null,
    scheduled_arrival   timestamptz not null,
    departure_airport   char(3)     not null,
    arrival_airport     char(3)     not null,
    status              varchar(20) not null,
    aircraft_code       char(3)     not null,
    actual_departure    timestamptz,
    actual_arrival      timestamptz,
    CHECK   ( scheduled_arrival > scheduled_departure ),
    CHECK   ( status IN ( 'On Time', 'Delayed', 'Departed',
         'Arrived', 'Scheduled', 'Cancelled' )
    ),
    CHECK   ( actual_arrival    IS null OR
        (
            actual_departure IS null AND
            actual_arrival IS NOT NULL AND
            actual_arrival > actual_departure
        )
    ),
    PRIMARY KEY (   flight_id   ),
    UNIQUE  (   flight_no, scheduled_departure   ),
    FOREIGN KEY (   aircraft_code   )
        REFERENCeS  aircrafts   (   aircraft_code   ),
    FOREIGN KEY (   arrival_airport   )
        REFERENCeS  airports   (   airport_code   ),
    FOREIGN KEY (   departure_airport   )
        REFERENCeS  airports  (   airport_code   )
);";
            await AddTable(sql);

            sql = @"
CREATE TABLE bookings
(   book_ref        char(6)         not null,
    book_date       timestamptz     not null,
    total_amount    numeric(10,2)   not null,    
    PRIMARY KEY (   book_ref   )
);";
            await AddTable(sql);

            sql = @"
CREATE TABLE tickets
(   ticket_no       char(13)        not null,
    book_ref        char(6)         not null,
    passenger_id    varchar(20)     not null,
    passenger_name  text            not null,    
    PRIMARY KEY (   ticket_no   ),
    FOREIGN KEY (   book_ref   )
        REFERENCES  bookings   (   book_ref   )
);";
            await AddTable(sql);

            sql = @"
CREATE TABLE ticket_flights
(   ticket_no       char(13)        not null,
    flight_id       integer         not null,
    fare_conditions varchar(10)     not null,
    amount          numeric(10,2)   not null,
    CHECK   ( amount >= 0   ),
    CHECK   ( fare_conditions IN 
        ( 'Economy', 'Comfort', 'Business' )
    ),
    PRIMARY KEY (   ticket_no, flight_id    ),
    FOREIGN KEY (   flight_id   )
        REFERENCeS  flights   (   flight_id   ),
    FOREIGN KEY (   ticket_no   )
        REFERENCeS  tickets   (   ticket_no   )
);";
            await AddTable(sql);

            sql = @"
CREATE TABLE boarding_passes
(   ticket_no       char(13)        not null,
    flight_id       integer         not null,
    boarding_no     integer         not null,
    seat_no         varchar(4)      not null,
    PRIMARY KEY (   ticket_no, flight_id    ),
    UNIQUE  (   flight_id, boarding_no  ),
    UNIQUE  (   flight_id, seat_no  ),
    FOREIGN KEY (   ticket_no, flight_id   )
        REFERENCES  ticket_flights   (   ticket_no, flight_id   )
);";
            await AddTable(sql);
        }

        private static async Task AddTable(string sql)
        {
            var connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (var con = new NpgsqlConnection(connection))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    var reader = await cmd.ExecuteNonQueryAsync();
                }
            }

            Console.WriteLine();
        }
    }
}
