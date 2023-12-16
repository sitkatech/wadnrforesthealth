



  -- q29 id 7
  -- q30 id 7
  -- q35 id 7
  -- q43 id 7
  update dbo.FindYourForesterQuestion set ForesterRoleID = 7
  where FindYourForesterQuestionID in (29,30,35,43)

  -- q18 test = 'I am interested in financial assistance for forest management, forest health, fuels mitigation, or post-fire recovery.'
  update dbo.FindYourForesterQuestion set QuestionText = 'I am interested in financial assistance for forest management, forest health, fuels mitigation, or post-fire recovery.'
  where FindYourForesterQuestionID = 18


  -- q26 bonus = '*DNR has specific staff certified to inspect tree farms. Your local Service Forester can connect you with the best person in your area.'

  update dbo.FindYourForesterQuestion set ResultsBonusContent = '*DNR has specific staff certified to inspect tree farms. Your local Service Forester can connect you with the best person in your area.'
  where FindYourForesterQuestionID = 26