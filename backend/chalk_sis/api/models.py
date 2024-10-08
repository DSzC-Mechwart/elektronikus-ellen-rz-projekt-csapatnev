from django.db import models
from django.contrib.auth.models import User

USER_ROLES = (
    ("admin", "Admin"),
    ("teacher", "Teacher"),
    ("student", "Student"),
)

# Create your models here.


class Profession(models.Model):
    name = models.CharField(max_length=255, unique=True)

    def __str__(self):
        return self.name


class Subject(models.Model):
    name = models.CharField(max_length=255)
    is_professional = models.BooleanField(default=False)
    grade = models.IntegerField(default=9)
    count_per_week = models.IntegerField(default=0)
    count_per_year = models.IntegerField(default=0)

    def __str__(self):
        return f"{self.name} ({str(self.grade)})"


class Class(models.Model):
    grade = models.IntegerField()
    letter = models.CharField(max_length=8)

    def __str__(self):
        return f"{self.grade}.{self.letter}"


class UserData(models.Model):
    for_user = models.OneToOneField(User, on_delete=models.CASCADE)
    role = models.CharField(max_length=255, choices=USER_ROLES)
    name = models.CharField(max_length=255)
    student_class = models.ForeignKey(to=Class, on_delete=models.CASCADE, blank=True, null=True)
    mothers_name = models.CharField(max_length=255, null=True, blank=True)
    place_of_birth = models.CharField(max_length=255, null=True, blank=True)
    date_of_birth = models.DateField(null=True, blank=True)
    address = models.CharField(max_length=255, null=True, blank=True)
    enrollment_date = models.DateField(null=True, blank=True, auto_now=True)
    profession = models.ForeignKey(to=Profession, on_delete=models.CASCADE, null=True, blank=True)
    subjects = models.ManyToManyField(Subject, blank=True)
    dorm_name = models.CharField(max_length=255, null=True, blank=True)
    is_first_login = models.BooleanField(default=True)
    student_number = models.IntegerField(null=True, blank=True)
    student_id = models.CharField(max_length=255, null=True, blank=True)

    def __str__(self):
        return f"{self.for_user.username} ({self.role})"


class Grade(models.Model):
    value = models.IntegerField(default=1)
    note = models.CharField(max_length=255, null=True, blank=True)
    date = models.DateField(auto_now=True)
    student = models.ForeignKey(User, on_delete=models.CASCADE, related_name="grades")
    subject = models.ForeignKey(Subject, on_delete=models.CASCADE, related_name="grades")

    def __str__(self):
        return f"{self.student} - {self.value} ({self.subject})"
