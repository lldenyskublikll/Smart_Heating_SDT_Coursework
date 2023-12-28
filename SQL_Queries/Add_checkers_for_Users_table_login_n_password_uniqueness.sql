ALTER TABLE Users
ADD CONSTRAINT Check_Login_Uniqueness UNIQUE (UserLogin);

ALTER TABLE Users
ADD CONSTRAINT Check_Password_Uniqueness UNIQUE (UserPassword);