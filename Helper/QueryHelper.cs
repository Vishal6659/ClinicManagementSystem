namespace ClinicManagementSystem.Helper
{
    public class QueryHelper
    {
        public const string insertRegistrationData = "Insert into tbl_newdocregistration (firstname, lastname, gender, dateofbirth, email, phonenumber, address, aadharcard, specialization, medicalliscence, qualification, experiance, affiliation, languagespoken, username, _password) values "
            + "(@Firstname, @Lastname, @Gender, @DateOfBirth, @EmailAddress, @PhoneNumber, @Address, @AadharCardNumber, @Specialization, @MedicalLiscense, @Qualificaition, @Experiance, @Affiliation, @LanguageSpoken, @Username, @Password);";

        public const string verifyLoginCredentials = "Select * from tbl_newdocregistration where username =@Username and _password =@Password;";
        public const string insertNewPatientData = "Insert into tbl_newpatient (doc_id, patient_id, firstname, lastname, patientage, phone, gender, presentcomplaint, pasthistory, familyhistory, presentmedication, physicalnature, mentalnature) values " + "(CAST(@DocId AS Int), CAST(@PatientId AS Int), @FirstName, @LastName, CAST(@Age AS Int), CAST(@Phone AS BigInt), @Gender, @PresentComplaint, @PastHistory, @FamilyHistory, @PresentMedication, @PhysicalNature, @MentalNature);";
        public const string getAllPatientListData = "Select * from tbl_newpatient where doc_id = @DocId::bigint;";
        public const string getAllPatientListDataForToday = "select * from tbl_newpatient tn where doc_id = @DocId::bigint and created_at >= current_date::timestamp ;";
        public const string insertNewDrugData = "insert into tbl_alldrug(doc_id, drugname, genericname, note) values " + "(CAST(@DocId AS Int), @DrugName, @GenericName, @Note);";
        public const string getAllDrugListData = "Select * from tbl_alldrug where doc_id = @DocId::bigint;";
        public const string insertNewTestData = "insert into tbl_alltests(doc_id, testname, description) values" + "(CAST(@DocId AS Int), @TestName, @Description);";
        public const string getAllTestListData = "Select * from tbl_alltests where doc_id = @DocId::bigint;";
        public const string getAllPatientCountForDashboard = "Select COUNT(*) from tbl_newPatient where doc_id = @DocId::bigint;";
        public const string getAllNewPatientCountForDashboard = "Select COUNT(id) from tbl_newpatient where doc_id = @DocId::bigint and created_at >= current_date::timestamp;";
        public const string insertNewAppointmentData = "insert into tbl_newappointment(doc_id, namee, datee, timee, status) values" + "(CAST(@DocId AS Int), @Name, @Date, @Time, @Status);";
        public const string getAllAppointmentListData = "Select * from tbl_newappointment where doc_id = @DocId::bigint;";
        public const string getAllAppointmentCountForDashboard = "Select COUNT(id) from tbl_newappointment where doc_id = @DocId::bigint;";
        public const string getAllPrescriptionCountForDashboard = "Select COUNT(id) from tbl_newprescription where doc_id = @DocId::bigint;";
        public const string getAllPaymentCountForToday = "select sum(amount) AS amount from tbl_newbilling where doc_id = @DocId::bigint and created_at >= current_date::timestamp;";
        public const string getAllPaymentCounts = "select sum(amount) AS amount from tbl_newbilling where doc_id = @DocId::bigint;";
        public const string getNewAppointmentCountForDashboard = "Select COUNT(id) from tbl_newappointment where doc_id = @DocId::bigint and createdat >= current_date::timestamp;";
        public const string getNewPrescriptionCountForDashboard = "Select COUNT(id) from tbl_newprescription where doc_id = @DocId::bigint and created_at >= current_date::timestamp;";
        public const string getAllPatientsNameByDocId = "select patient_id, firstname from tbl_newpatient tn where doc_id = @DocId::bigint;";
        public const string getAllDrugNameByDocId = "select drugname from tbl_alldrug tn where doc_id = @DocId::bigint;";
        public const string getAllTestNameByDocId = "select testname from tbl_alltests tn where doc_id = @DocId::bigint;";
        public const string insertNewPrescriptionData = "insert into tbl_newprescription(patient_id, patient_name, doc_id, drug_type, drug_name, drug_mgorml, drug_dosage, test_name, test_description) values" + "(CAST(@PatientId AS Int), @PatientName, CAST(@DocId AS Int), @DrugType, @DrugName, @DrugMgOrMl, @DrugDosage ,@TestName, @TestDescription)";
        public const string getAllPriscriptionListData = "select distinct on(patient_id) id, patient_id, patient_name, drug_name, drug_dosage, test_name, test_description from tbl_newprescription where doc_id = @DocId::bigint;";
        public const string getAllPastRecordsOfPatientByPatientId = "select * from tbl_newprescription where doc_id = @DocId::Int and patient_id = @PatientId::bigint;";
        public const string insertNewBillingData = "insert into tbl_newbilling(patient_id, doc_id, patient_name, payment_mode, amount, status) values " + "(CAST(@PatientId AS Int), CAST(@DocId AS Int), @PatientName, @PaymentMode, CAST(@Amount AS Int), @PaymentStatus);";
        public const string getAllBillingListData = "select id, patient_id, patient_name, amount, status, created_at from tbl_newbilling where doc_id = @DocId::bigint;";

        public const string deletePatientRecord = "delete from tbl_newpatient where doc_id = @DocId::Int AND id = @RecordId::Int;";
        public const string deletePrescriptionRecord = "delete from tbl_newprescription where doc_id = @DocId::Int AND patient_id = @PatientId::Int AND id = @RecordId::Int;";
        public const string deleteBillingRecord = "delete from tbl_newbilling where doc_id = @DocId::Int AND patient_id = @PatientId::Int AND id = @RecordId::Int;";
        public const string deleteAppointmentRecord = "delete from tbl_newappointment where doc_id = @DocId::Int AND id = @RecordId::Int;";
        public const string deleteDrugRecord = "delete from tbl_alldrug where doc_id = @DocId::Int AND id = @RecordId::Int;";
        public const string deleteTestRecord = "delete from tbl_alltests where doc_id = @DocId::Int AND id = @RecordId::Int;";
    }
}