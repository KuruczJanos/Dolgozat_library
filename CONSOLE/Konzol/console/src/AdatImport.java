import java.io.File;
import java.io.FileNotFoundException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Scanner;

public class AdatImport {



    public static void importalSQLFajl(String eleresiUtvonal) {
        try {
            File file = new File(eleresiUtvonal);
            Scanner scanner = new Scanner(file);
            StringBuilder sb = new StringBuilder();
            while (scanner.hasNextLine()) {
                sb.append(scanner.nextLine());
            }
            String sqlUtasitasok = sb.toString();

            Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/kolcsonzesek", "root", "");
            Statement statement = connection.createStatement();
            statement.execute(sqlUtasitasok);

            statement.close();
            connection.close();
            scanner.close();
        } catch (FileNotFoundException | SQLException e) {
            e.printStackTrace();
        }
    }

    public static void importalKolcsonzok(String eleresiUtvonal, String tablaNeve) {
        int sikeresSorok = 0;
        try {
            File file = new File(eleresiUtvonal);
            Scanner scanner = new Scanner(file);
            Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/kolcsonzesek", "root", "");
            Statement statement = connection.createStatement();

            // Kihagyjuk az első sort, mert az az oszlopneveket tartalmazza
            if (scanner.hasNextLine()) {
                scanner.nextLine();
            }

            while (scanner.hasNextLine()) {
                String line = scanner.nextLine();
                String[] lineArray = line.split(";");
                StringBuilder insertQuery = new StringBuilder("INSERT INTO ");
                insertQuery.append(tablaNeve);
                insertQuery.append("(nev, szulIdo) VALUES ( ");
                for (int i = 0; i < lineArray.length; i++) {
                    insertQuery.append("'");
                    insertQuery.append(lineArray[i]);
                    insertQuery.append("'");
                    if (i < lineArray.length - 1) {
                        insertQuery.append(", ");
                    }
                }
                insertQuery.append(")");
                sikeresSorok += statement.executeUpdate(insertQuery.toString());
            }

            statement.close();
            connection.close();
            scanner.close();
        } catch (FileNotFoundException | SQLException e) {
            e.printStackTrace();
        }
        System.out.println("A sikeresen importált sorok száma " + tablaNeve + " táblába: " + sikeresSorok);
    }

    public static void importalKolcsonzesek(String eleresiUtvonal, String tablaNeve) {
        int sikeresSorok = 0;
        try {
            File file = new File(eleresiUtvonal);
            Scanner scanner = new Scanner(file);
            Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/kolcsonzesek", "root", "");
            Statement statement = connection.createStatement();

            // Kihagyjuk az első sort, mert az az oszlopneveket tartalmazza
            if (scanner.hasNextLine()) {
                scanner.nextLine();
            }

            while (scanner.hasNextLine()) {
                String line = scanner.nextLine();
                String[] lineArray = line.split(";");
                StringBuilder insertQuery = new StringBuilder("INSERT INTO ");
                insertQuery.append(tablaNeve);
                insertQuery.append("(kolcsonzokId, iro, mufaj, cim) VALUES ( ");
                for (int i = 0; i < lineArray.length; i++) {
                    insertQuery.append("'");
                    insertQuery.append(lineArray[i]);
                    insertQuery.append("'");
                    if (i < lineArray.length - 1) {
                        insertQuery.append(", ");
                    }
                }
                insertQuery.append(")");
                sikeresSorok += statement.executeUpdate(insertQuery.toString());
            }

            statement.close();
            connection.close();
            scanner.close();
        } catch (FileNotFoundException | SQLException e) {
            e.printStackTrace();
        }
        System.out.println("A sikeresen importált sorok száma " + tablaNeve + " táblába: " + sikeresSorok);
    }

    public void AdatIMP(){
        // SQL fájl importálása
        importalSQLFajl("kolcsonzesek.sql");

        // CSV fájlok importálása
        importalKolcsonzok("Kolcsonzok.csv", "kolcsonzok");
        importalKolcsonzesek("Kolcsonzesek.csv", "kolcsonzesek");
    }
}


