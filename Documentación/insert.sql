INSERT INTO REQUESTERS VALUES('4.444.111-4', 'Pepe', 'Apellido', 'pepe@gmail.com', '099999999');
INSERT INTO REQUESTERS VALUES('4.333.222-4', 'Lala', 'LalaApellido', 'lala@gmail.com', '099111999');
INSERT INTO REQUESTERS VALUES('3.666.333-1', 'PepeJr', 'PepeJrApellido', 'pepejr@gmail.com', '099222999');
INSERT INTO REQUESTERS VALUES('4.999.555-4', 'LalaJr', 'LalaJrApellido', 'lalajr@gmail.com', '099999449');


INSERT INTO CASES VALUES('email@email.com', '2011-04-12T00:00:00.000', 0, 1, 1)
INSERT INTO CASES VALUES('rara@email.com', '2014-01-12T00:00:00.000', 1, 2, 2)
INSERT INTO CASES VALUES('cccc@email.com', '2016-09-03T00:00:00.000', 1, 3, 3)
INSERT INTO CASES VALUES('vvvv@email.com', '2018-02-03T00:00:00.000', 0, 4, 4)

UPDATE Stages SET documentName = 'pepe.jpeg' WHERE id = 1;
UPDATE Stages SET documentName = 'lala.jpeg' WHERE id = 2;