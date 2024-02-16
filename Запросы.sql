--Сотрудник с максимальной заработной платой
SELECT *
FROM Employee
WHERE SALARY = (SELECT MAX(SALARY) FROM Employee);

--Максимальная длина цепочки руководителей по таблице сотрудников (вычислить глубину дерева)
WITH RECURSIVE EmployeeTree AS (
    SELECT ID, CHIEF_ID, 1 AS Level
    FROM Employee
    WHERE CHIEF_ID IS NULL
    
    UNION ALL
    
    SELECT e.ID, e.CHIEF_ID, et.Level + 1
    FROM Employee e
    JOIN EmployeeTree et ON e.CHIEF_ID = et.ID
)
SELECT MAX(Level) AS MaxDepth
FROM EmployeeTree;

--Отдел, с максимальной суммарной зарплатой сотрудников
SELECT Department.NAME AS Department_Name, SUM(Employee.SALARY) AS Total_Salary
FROM Employee
JOIN Department ON Employee.DEPARTMENT_ID = Department.ID
GROUP BY Department.NAME
ORDER BY Total_Salary DESC
LIMIT 1;

--Сотрудник, чье имя начинается на «Р» и заканчивается на «н»
SELECT *
FROM Employee
WHERE NAME LIKE 'Р%н %' OR NAME LIKE 'р%н %';




