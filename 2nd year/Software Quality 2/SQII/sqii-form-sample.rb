require 'watir'
require 'test/unit'

class TC_TEST_SQII_FORM < Test::Unit::TestCase
     def test_id_1
	puts ("\n\nSQII-Form : Test Case ID 1")
	puts ("      Step 1: Go to the SQII-Form Site : http://localhost")
		@browser = Watir::Browser.start "http://localhost"
	puts("      Step 2: Set various element values")
		@browser.select_list(:name, "MDSAB2A").select_value("00")
	puts("      Step 3: Submit the form")
		@browser.form().submit
	puts("      Step 4: Validate the test")
	begin
	   assert(@browser.hidden(:name, "ValidPatient").value == "0", "No Problem")
		puts("        TEST PASSED. Expected ValidPatient = 0")
	   rescue Exception => e
		puts("        TEST FAILED. Exception : " + e.message+"\n")
	end
	begin
		assert(@browser.hidden(:name, "NumberOfErrors").value == "5", "NumberOfErrors 5")
		 puts("        TEST PASSED. Expected ValidPatient = 0, NumberOfErrors = 5")
		rescue Exception => e
		 puts("        TEST FAILED. Exception : " + e.message+"\n")
	end
	@browser.close()
   end 
   
   def test_id_2
	puts ("\n\nSQII-Form : Test Case ID 2")
	puts ("      Step 1: Go to the SQII-Form Site : http://localhost")
		@browser = Watir::Browser.start "http://localhost"
		
	puts("      Step 2: Set various element values")
	puts("         Put MDSAB2A")
		@browser.select_list(:name, "MDSAB2A").select_value("01")
	puts("         Put MDSAB3-1")
		@browser.radio(:id, "MDSAB3-1").set
	puts("         Put MDSAB4")
		@browser.text_field(:name, "MDSAB4").set("D3D3D3")
	puts("         Put MDSAB5f")
		@browser.checkbox(:name, "MDSAB5f").set
	puts("         Put MDSAB8")
		@browser.text_field(:name, "MDSAB8").set("eng")
	puts("         Put MDSAB9-1")
		@browser.radio(:id, "MDSAB9-1").set
	puts("         MDSAB10a to checked")
		@browser.checkbox(:name, "MDSAB10a").set
	puts("      Step 3: Submit the form")      
		@browser.form().submit
	
	puts("      Step 4: Validate the test")
	
	begin
		assert(@browser.hidden(:name, "ValidPatient").value == "0", "Patient valid")	
		puts("        TEST #1 PASSED. Expected ValidPatient = 0")			
	   rescue Exception => e
		puts("        TEST #1 FAILED. Exception : " + e.message+"\n")
	end
	begin 
		assert(@browser.hidden(:name, "NumberOfErrors").value == "3", "No 3 Error")
		puts("        TEST #2 PASSED. NumberOfErrors = 3")
		rescue Exception => e
		puts("        TEST #2 FAILED. Exception : " + e.message+"\n")
	end
	begin
		assert(@browser.table(:text => /Canada Post/), "No Canada Post")
		puts("        TEST #3 PASSED. No Canada Post")
		rescue Exception => e
		puts("        TEST #3 FAILED. Exception : " + e.message+"\n")
	end	
	
	@browser.close()
   end
   
   def test_id_3
	puts ("\n\nSQII-Form : Test Case ID 3")
	puts ("      Step 1: Go to the SQII-Form Site : http://localhost")
		@browser = Watir::Browser.start "http://localhost"
		
	puts("      Step 2: Set various element values")
	puts("         Put MDSAB2A")
		@browser.select_list(:name, "MDSAB2A").select_value("01")
	puts("         Put MDSAB3-1")
		@browser.radio(:id, "MDSAB3-1").set
	puts("         Put MDSAB4")
		@browser.text_field(:name, "MDSAB4").set("D3D3D3")
	puts("         Put MDSAB5f")
		@browser.checkbox(:name, "MDSAB5f").set
	puts("         Put MDSAB8")
		@browser.text_field(:name, "MDSAB8").set("eng")
	puts("         Put MDSAB9-1")
		@browser.radio(:id, "MDSAB9-1").set
	puts("         MDSAB10a to checked")
		@browser.checkbox(:name, "MDSAB10a").set
	puts("         MDSAB2B to checked")
		@browser.text_field(:name, "MDSAB2B").set("V0002")
	puts("         MDSAB7 to checked")
	@browser.select_list(:name, "MDSAB7").select_value("9")
	puts("      Step 3: Submit the form")      
		@browser.form().submit
	
	puts("      Step 4: Validate the test")
	
	begin
		assert(@browser.hidden(:name, "ValidPatient").value == "0", "Patient valid")	
		puts("        TEST #1 PASSED. Expected ValidPatient = 0")			
	   rescue Exception => e
		puts("        TEST #1 FAILED. Exception : " + e.message+"\n")
	end
	begin 
		assert(@browser.hidden(:name, "NumberOfErrors").value == "1", "No 1 Error")
		puts("        TEST #2 PASSED. NumberOfErrors = 1")
		rescue Exception => e
		puts("        TEST #2 FAILED. Exception : " + e.message+"\n")
	end
	begin
		assert(@browser.table(:text => /Invalid/), "AB4 Valid")
		puts("        TEST #3 PASSED. Invalid")
		rescue Exception => e
		puts("        TEST #3 FAILED. Exception : " + e.message+"\n")
	end	
	
	@browser.close()
   end
   
    def test_id_4
	puts ("\n\nSQII-Form : Test Case ID 4")
	puts ("      Step 1: Go to the SQII-Form Site : http://localhost")
		@browser = Watir::Browser.start "http://localhost"
		
	puts("      Step 2: Set various element values")
	puts("         Put MDSAB2A")
		@browser.select_list(:name, "MDSAB2A").select_value("01")
	puts("         Put MDSAB3-1")
		@browser.radio(:id, "MDSAB3-1").set
	puts("         Put MDSAB4")
		@browser.text_field(:name, "MDSAB4").set("D3D3D3")
	puts("         Put MDSAB5f")
		@browser.checkbox(:name, "MDSAB5f").set
	puts("         Put MDSAB8")
		@browser.text_field(:name, "MDSAB8").set("eng")
	puts("         Put MDSAB9-1")
		@browser.radio(:id, "MDSAB9-1").set
	puts("         Put MDSAB10a")
		@browser.checkbox(:name, "MDSAB10a").set
	puts("         Put MDSAB2B")
		@browser.text_field(:name, "MDSAB2B").set("V0002")
	puts("         Put MDSAB7")
		@browser.select_list(:name, "MDSAB7").select_value("9")
	puts("         Put MDSAB4")
	@browser.text_field(:name, "MDSAB4").set("X0X0X0")	
	puts("      Step 3: Submit the form")      
		@browser.form().submit
	
	puts("      Step 4: Validate the test")
	
	begin
		assert(@browser.hidden(:name, "ValidPatient").value == "1", "Patient is not valid")	
		puts("        TEST #1 PASSED. Expected ValidPatient = 1")			
	   rescue Exception => e
		puts("        TEST #1 FAILED. Exception : " + e.message+"\n")
	end
	begin 
		assert(@browser.hidden(:name, "NumberOfErrors").value == "0", "1 Error")
		puts("        TEST #2 PASSED. NumberOfErrors = 1")
		rescue Exception => e
		puts("        TEST #2 FAILED. Exception : " + e.message+"\n")
	end
	@browser.close()
   end
   
    def test_id_5
	puts ("\n\nSQII-Form : Test Case ID 5")
	puts ("      Step 1: Go to the SQII-Form Site : http://localhost")
		@browser = Watir::Browser.start "http://localhost"
		
	puts("      Step 2: Set various element values")
	puts("         Put MDSAB2A")
		@browser.select_list(:name, "MDSAB2A").select_value("10")
	puts("         Put MDSAB3")
		@browser.radio(:name, "MDSAB3").set
	puts("         Put MDSAB4")
		@browser.text_field(:name, "MDSAB4").set("N0B")
	puts("         Put MDSAB5a")
		@browser.checkbox(:name, "MDSAB5a").set
	puts("         Put MDSAB7")
		@browser.select_list(:name, "MDSAB7").select_value("4")
	puts("         Put MDSAB8")
		@browser.text_field(:name, "MDSAB8").set("fre")
	puts("         Put MDSAB9")
		@browser.radio(:name, "MDSAB9").set
	puts("         Put MDSAB10a")
		@browser.checkbox(:name, "MDSAB10a").set
		@browser.form().submit
	begin
		assert(@browser.hidden(:name, "ValidPatient").value == "1", "Patient is not valid")	
		puts("        TEST #1 PASSED. Expected ValidPatient = 1")			
	   rescue Exception => e
		puts("        TEST #1 FAILED. Exception : " + e.message+"\n")
	end
	begin 
		assert(@browser.hidden(:name, "NumberOfErrors").value == "0", "1 Error")
		puts("        TEST #2 PASSED. NumberOfErrors = 1")
		rescue Exception => e
		puts("        TEST #2 FAILED. Exception : " + e.message+"\n")
	end
		@browser.close()
   end
end
