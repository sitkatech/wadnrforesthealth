
# coding: utf-8
# This code was pulled from a Jupyter Notebook. If fuzzy matching task is more than a one-off, may be worth fully fleshing as python script
import operator
import pandas as pd
import Levenshtein as matcher # This is 3rd party lib v. 0.12.0 from https://pypi.org/project/python-Levenshtein/


result = pd.read_excel('FuzzyMatchingIn.xlsx', 'Sheet2')
result_df = pd.DataFrame(result).dropna()
# make dictionary of form tuple:value. Specifically, (region, summary name, grant name): match ratio
ratio_dict = {}
for region, summary_name in zip(result_df['REGION'], result_df['SUMMARY']):
    #print(f'{summary_name}, {type(summary_name)}')
    for grant_name in result_df['GRANTS']:
        #print(f'{grant_name}, {type(grant_name)}')
        ratio_dict[(region, summary_name, grant_name)] = matcher.ratio(summary_name, grant_name)
sorted_x = list(reversed(sorted(ratio_dict.items(), key=operator.itemgetter(1))))
# Arbitrarily setting ratio cut-off at 0.65
unpacked = [[i[0],i[1],i[2],j] for i,j in sorted_x if j > 0.65]
df_output = pd.DataFrame(unpacked,columns=['REGION', 'SUMMARY','GRANTS','Levenshtein Distance Ratio'])
# Clobbers input sheet, but ideally would eventually write to new, different sheet.
writer = pd.ExcelWriter('FuzzyMatchingOut.xlsx')
df_output.to_excel(writer, 'Sheet3')
writer.save()

# Helper for jupyter notebook to convert interactive work to .py file
get_ipython().system('jupyter nbconvert --to script FuzzyMatchingGrants.ipynb')



