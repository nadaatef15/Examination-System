$(document).ready(function () {
  
    $('#mcqForm').collapse('hide');
    $('#trueFalseForm').collapse('hide');


    $('#selectquestionType').change(function () {
        var selectedType = $(this).val();
        $('.questionType').val(selectedType);
        if (selectedType === 'MCQ') {
            $('#mcqForm').collapse('show');
            $('#trueFalseForm').collapse('hide');
        } else if (selectedType === 'TF') {
            $('#trueFalseForm').collapse('show');
            $('#mcqForm').collapse('hide');
        }
    });
    $('#selectedCourse').change(function () {
        var selectedCourseId = $(this).val();
        $('.courseId').val(selectedCourseId); 
    });

});