--INSERT INTO 
--    dbo.StuProfiles (FirstName, LastName, Bio, Year, Speciality, SchoolId)
--VALUES 
--    ('NonAdmin','Test','Short bio about this test person.','Sophomore','Pediatric','1'),
--    ('Admin','Test','Short bio about this test person.','N/A','N/A','1');

--INSERT INTO
--    dbo.Schools (Name, Website, Address, LatLong, EmailDomain)
--VALUES
--    ('USC School of Medicine', 'http://www.med.sc.edu/', '6311 Garners Ferry Rd, Columbia, SC 29209', '33.978735, -80.962941', '@uscmed.sc.edu');

INSERT INTO
    dbo.Trips (Title, Destination, DestLongLat, Dates, Deadline, TargetAmnt, PercentOfAmnt, IsActive)
VALUES
    ('Cervicusco in Peru', 'Cusco, Peru', '-13.544288, -71.904130', 'July 10 - July 24 2017', '2017-06-01 00:00:00.000', '2850',0, 1);