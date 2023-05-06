namespace ClinicManagementSystem.Helper
{
    public class QueryHelper
    {
        public const string insertRegistrationData = "Insert into tbl_registration (firstname, middlename, lastname, age, mobilenumber, email, address, officename, landmark, city, state, country, gender, username, _password, confirmpassword) values "
            + "(@Firstname, @Middlename, @Lastname, @Age, @Mobilenumber, @Email, @Address, @Officename, @Landmark, @City, @State, @Country, @Gender, @Username, @Pass, @Confirmpassword);";

        public const string verifyLoginCredentials = "Select * from tbl_registration where username =@Username and _password =@Password;";
        public const string insertNewPatientData = "Insert into tbl_newPatient (firstname, lastname, age, phone, birthday, gender, bloodgroup, address, patientweight, patientheight) values " + "(@FirstName, @LastName, @Age, @Phone, @Birthday, @Gender, @BloodGroup, @Address, @PatientWeight, @PatientHeight);";
    }
}
