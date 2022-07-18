using System.Collections.Generic;
using NUnit.Framework;

namespace ProjectFirma.Web
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class ADFSSamlResponseTest
    {
        private const string AdfsSampleSamlBase64Encoded = "PHNhbWxwOlJlc3BvbnNlIElEPSJfOTUyZjRiNDYtNjE2OS00Njg1LWJmMWUtOTg2N2ZmZjNiMGI3IiBWZXJzaW9uPSIyLjAiIElzc3VlSW5zdGFudD0iMjAyMS0wMy0xMlQxODoyNDoyMi45NDBaIiBEZXN0aW5hdGlvbj0iaHR0cHM6Ly9mb3Jlc3RoZWFsdGh0cmFja2VyLmRuci53YS5nb3YvQWNjb3VudC9BREZTUG9zdCIgQ29uc2VudD0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOmNvbnNlbnQ6dW5zcGVjaWZpZWQiIHhtbG5zOnNhbWxwPSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6cHJvdG9jb2wiPjxJc3N1ZXIgeG1sbnM9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDphc3NlcnRpb24iPmh0dHA6Ly9lYWQuc3RzLndhLmdvdi9hZGZzL3NlcnZpY2VzL3RydXN0PC9Jc3N1ZXI+PHNhbWxwOlN0YXR1cz48c2FtbHA6U3RhdHVzQ29kZSBWYWx1ZT0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOnN0YXR1czpTdWNjZXNzIiAvPjwvc2FtbHA6U3RhdHVzPjxFbmNyeXB0ZWRBc3NlcnRpb24geG1sbnM9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDphc3NlcnRpb24iPjx4ZW5jOkVuY3J5cHRlZERhdGEgVHlwZT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxlbmMjRWxlbWVudCIgeG1sbnM6eGVuYz0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxlbmMjIj48eGVuYzpFbmNyeXB0aW9uTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxlbmMjYWVzMjU2LWNiYyIgLz48S2V5SW5mbyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnIyI+PGU6RW5jcnlwdGVkS2V5IHhtbG5zOmU9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMDQveG1sZW5jIyI+PGU6RW5jcnlwdGlvbk1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMDQveG1sZW5jI3JzYS1vYWVwLW1nZjFwIj48RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnI3NoYTEiIC8+PC9lOkVuY3J5cHRpb25NZXRob2Q+PEtleUluZm8+PGRzOlg1MDlEYXRhIHhtbG5zOmRzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjIj48ZHM6WDUwOUlzc3VlclNlcmlhbD48ZHM6WDUwOUlzc3Vlck5hbWU+Q049c3NvLmZvcmVzdGhlYWx0aHRyYWNrZXIuZG5yLndhLmdvdiwgTz1TaXRrYSBUZWNobm9sb2d5IEdyb3VwIExMQywgTD1Qb3J0bGFuZCwgUz1PcmVnb24sIEM9VVM8L2RzOlg1MDlJc3N1ZXJOYW1lPjxkczpYNTA5U2VyaWFsTnVtYmVyPjEyOTU5ODkyNzQ0OTQxMjgyNjI1MDc2NzgxMzg5MzgwNTA3ODc5ODwvZHM6WDUwOVNlcmlhbE51bWJlcj48L2RzOlg1MDlJc3N1ZXJTZXJpYWw+PC9kczpYNTA5RGF0YT48L0tleUluZm8+PGU6Q2lwaGVyRGF0YT48ZTpDaXBoZXJWYWx1ZT5pdGtJZGt0THEyWE85eDlBT3RIejVJTmdUZzJiblBONzFMdEc5NVBCUWdZY242dU45YUVlNlJYOUNGeW9KRC84ZmYrVWR2SERkQTdINW1xS1EvODRxUzFsRU11UkNZL3dOZ1BUMGxITzBLSDJCaHhaMnBncDRkZGV4KzYyZ1NKV1g1eWlZR3owZWpHMmtZMjh2VU8zT2doVXBBL25PWXA2YXIzM1lQcDU2dmt5MjhkWVNYNG9UWnZrVkMrU1poM2JLNWhtVHl0UThoUDJqZ3ZQWW1pWGFXYThWd1FnbUFlRi9wVzlsNFFYS25mQkV2SVRENFNqaFlkNXhCYjdxS2p4L25Bd1pGQ2NGR2dsczBZU3drSFhCQmxZdmdXRUZpOWhiZmtnY1d6SFdXSTlNRjZTYU5UaGRoODR0NGJORkcvUmZBbEJ3aVNkY3FhcGlaZDgyY254WVE9PTwvZTpDaXBoZXJWYWx1ZT48L2U6Q2lwaGVyRGF0YT48L2U6RW5jcnlwdGVkS2V5PjwvS2V5SW5mbz48eGVuYzpDaXBoZXJEYXRhPjx4ZW5jOkNpcGhlclZhbHVlPjg0b29LVS82eFBQaU1lYUdHb2k0VXFVNUlEVCtVbFRpOXhZMTBkOHZnUTBmd0I1VUpnVDFNcXlCT25ZWkVMbUx2emg4Tm80WTY5ZnhFbktKVGJJM2NGOVgxL0N4UWxqYWxvL2NrU0FIMnAwdlUxV3R3MGx6SDIxSXpsaHphTExxQzRnUCtuZTczZnZRMWNLeE9teElndHZBa0NiTVUyNUNqcnNrRFVHMmxVV0R1OHJsRDBFV2svdHNxMFhqUm9XRkZaYTBmaVJJMkU5Q0xtMk9INms0aTllR3VHQ1Rrc1dPeGxZTEpPRW0vOEdBK3JhQ3VkYkVvQVFPRmlOb09ybXZKcC9aY3R5SS9za21pQ2MrREFmWE1ia2t3V3dQcHBkWnJrTHJSeWxYdEQ1NlVrMnBXMTV1Z2gvTmIxcmRmV3VYbm02RWRZa3hvaDMyV0lzTWJKcG4rMHFQSkhabFhIanNLYXQxQ0VZQnE1U3A2RzFuTitSNWtTeFl1RzFKMmN1Z2JJRHdEYzZvazJmVGhoQmJhRGxUd1I1ZVRtWTE3UlFMei9EeVhNYnpzbE9VSmE0MXdDRi9QbFB4Y3NybVlsb0Z5VmxGaWd5ekNBVWliMlQ4YU5zRnlkMlNLVW1vWm9LSmoxbVZZSlZUWm4rN1FJTUliYi94bVYyeExOdE9RdzFrdVEyWDZQcHRDTXZlakkzYjRDT3RQSDdtVEh1MVpKQlNaTXBrR2s3VVhNaGY5amRBeHd2ckZ5My93ZFZoZjhheDUxVTZzcWp4elJhQTJWVXZFYlU5ckxqMFBFbkRFNGdHNno0UFgyZWdhQTdGNXU1UWlDNjl1dGRTWU40eFlDbHJ1ZEhwUFZsU1pjS1B3STZqMGtoaVBmZEY2eENlOUNhMVVrRTNIV1BhMHhsNHY0aEZTR252ZVlPb2FWaXVXbU05WStsaS81aGw1QmZ5OUtvbjVLU21JVFRxYnVSSDE1ZncxV0NuazltWkVHMlFzUmg0UXBuVzJKbzRScGhTSTlIUExUY0RrZytDVXNSang5YTcwWGkyVi9TYW5yM1JzWmZwWU9qTFZCUjJYWWw4c1NWdWNPdTQzTzNmMS9CSkNxWmlWelJnaHBteFIzcHZOZy9tWlNnbExWK1RkTVdXa2p2Q0Z6M1B3amYwMnRUbGd2aEYybHN3M1ZGamluR0tzOG11dkRabFN5WEJLVVpiQUZZdzhid25heTIzcFJLakFkcVdPWDhadnpGMFRxK3diKzNQVVhBaUpkdmU3SXl0MXVjNXVEMCswckZGWlJYRU9CRFJMQnhZdWU2a2JLdVlDQkh5NE5PLzR1czBaN1prSTZlV2NTM0l6QTdvdVlSOGJvRmphUGlwMzk5bHlnM3I4ZU5yU2NlNExUT2JFNTROVWdFU0Q0Ri9Xck1OVjlFQWU3a2FJME42SjZjSlVCRXEvaTNhbVVRaTFrdnpQK3hzM1JZdUJMMDFsODRTcHNVWnZvblFrbzMyQ1dWbXptZFFqVE9MMW1rZ0NidjJzTGlpR0NKbHc0aTkvL0pWZWlGUmhSdC81NjNKT0NycHNMU0Y5d3ZKRVZUQzFtN09aU0JYZmdHVnB3MTNvNXRIMjlpRmFYdEpEeCszYjJ3dW5yZGJSSkwyYXg5RlozVFJaNkc4cFJCSnU4ZmVZajMvMktLSzlzMElOZGhTNjZiWFVWNnkrK0dROGt0Um1qL2I2azNZYVFCRy9tR1FjUU1WUUdEekFwYUk3SUNtd1Q5R1Q4MWloTnlkQkFnMUxlQ0VRU01PRXZYSW5ZeVpMODRLeGFhR2cxMm1TMUhvRkxQOUsxdTFTZ2Q3bG9yM01mQ00yMEdRQVdqWlZ2L1BIQ1N1QmFmd2JhZjYwcGtEUWpmeHhTa2xxWnJ5WEl3RjBhL29JMUxuSG5MVjRUeGFTK2Q0QW9ObXFwMHIyQlRhWGNLb0N5RkhGdlBCZW9ZZTBHNDlkNlRTc1BzbzRsTzlTYUwrQVFzT3VuakZCTGZwRDRaQ0hDTGU3N2dGNmIyTFp4NURKWnh3SDVNQXNTK1JwbWFWZFNzQTFzYmFyeFNRSTlwK2JRWWY5aFVXOGdxN0JhRG1VUHhEeG5VNjZSVkRCcHRxNGwycHJuZzBIZExSS2xFeVgxV1VVaVNrY0VlblU0QWJ5ME15dW1rWjdCSDg2SFJ1NjlnNWx0TmZNVDVaTnQwcDlnMlNzN01uSlo3ZWVqME5KN2hWcElmOE9VSFllVGdqeVRNa0lDTWFHbzdaU0dHUitNV0c4bk9LdFl2R3dRZnpZK2wxa3ZrZTZEYWpBdER6QnZMR21hZlVvelcyUFdmekthUk0xUEJ6a1hMWkZBbnJTT1cwbnhJWWo5cXVCNzJEWUZxdUJBL2RRYXppb0RDekw4dzZJb0c1VmNZTUZqWGFCNWhvMXNOT21RTW0xOTNqb0JVQVhaaVIzRjVoNGZxSE15R3pXOHJjVnowYnlQK3J2TkVyT3d2U1JtOUIvc0NLQmVVSjBDZDVJVUFJcnl2V1BFYldJNlEyRytMZ2UwKzQzci9SY1RFQjIrZzFaYXpDRjBERmJBWTNwM1ljZk80UlVueWRLTDhTVjVMSjAvUWhIVjBxdzkxRGlLRXN4NDJXc2xaaVJDbkNoNEV4ZXM4YXhkZ09rR2FqMDFNYWlVcURERksrbWdOd1N1SlVOVjlHSnJnYVkxbENBVlp3RlhLQlNYUGk1d0hjQXQ4MlliOVAwUnVsMlFkNlA3TGhyeHc5akppd3Z2LzJKUTRXRjErWjgrSWpTSFZlMTRFVVhnMUNFR2VRU040cXBpSHFPL2pmWE1lbEUwRkJVRFZjaDJlbTJIaVE3eXEyVzdyZlQyMThweE5HZTFnN0VpeHRiU1VKbnYrTkNBcGxRS2QrY0ZUNFl0cm1lTi9ibklSR3N5dWpWSHh0eENGZGpVTWlPNytFYzZWN1ZTWmhtN1ViQVpLbEpRN2k3bFRiN2gzRFFsb1grdUQ4anpSRUpUMEUxOVVkSFdFbUgwVWJnYzF1Q0VSQnEyazd3K0E3cmtFc3NjdjZReGxXb1ErRG9sOFJZTllMczdqUTlvUkwwMjR0S1ZUKzBZcjZ0NjhhOWg1MGdicXBaVXl6MWpvRGFubnI3R0dzZXRDQzh0M0t4cmY2V1FmZnh2aTFod1VxWGR3R3dHMFJ4M1E4QXFwdlZQQzJTekVpV2l6YTRwS0xHbXpiSmJhTjNxY2lwRERWTyt4OVB6cTlReUlNUDNkdW44RW55alNJelY5N2pBbTRidzN5blFPRGRPbHlOUHIrRW5aaG5GOHBhaVVpZmtpblFaR3NvREdXU1JqWk5ieHIveFJyNTJEL3MyZmFnMzNxMlI0ekltbmZlRy91Q1lOT05NT1RCTzF1MWxDVUw3ODhSaFY0S3RHVGdseHdRcjZ5Mm5zcmU2NlNDR0ZXZ1k3cGdYdWhSeVNHZ3FJUGlxZlBZTXVJSzh6ZXB6aVQyQUNudGlwTUNsbG9MakhzYllCNnl6UDdxaWM2L21TTGJ2b2tlMkhESm16eU82MzZBTUgyNGtHYzd2YUdENUxEYkhuMmRKTkM0ci8xOFZYeFRyVFlUWmYrZTE2VG9ZZjluOGEyMkw4bDY5OVpyZHFtMHlhQ2h0QUVKQlhRWXFnYzJxWjVUUEJ4K21HelR4UGdMR2lUSU5HcXhQTnQ1cE5uM0hZK1NCOTZ1ZzMzcVZsdFRIYVRjSTdTVnFMbHI1VWQvN1YxTHRlYno4NlFwalQwR3kvb2ZPNUxVbnhuaERYZzdiTXI3ZjFkWW4zWElOdVcyaVF0OHo0M2ZONE9QeGVpWnhsWUxjdUU1bkViNi9DeDdUUUNJZTdwUnM4Yyt0RnFQTW5hQlVjUWhZVS9Fenk5YWxQUndlQ2pLSVpRVCt6UHRlK1d6NHBIL21GZnBIZDdMemh3R0VQVm1GVzNvdURCbXphZ0VocU11U0ZHMFVHbHJiNG5RWFJZcEc5d0dlNlhEeHBubUxTaytXK1B3RlcxMVVsTTE5cU9iTFVIMk5wRk1yRVBqSmk4S2YyMHJIMWoxblJDVmpiNGNBWVBmS0dxU0tRbk9tTmpOQnRsTUtGNThVdVNBU0hrMDNDb2NWWEgyWW1jNXZnb2xsNkNjWTZwOXkrZld3T204aG5FditrN0N3R3EyYThxRWYxS3dtYkM1N0ZHTkVHclR3RVpTMlkyN1poZUNGNU85RlBmSHJYSkZ3eDBudjdLOUZuOWIxcTN2cWt2OEtWVkNQSnErbG5ZVWV2YnFFbDZhdUZaS251Si9QWHpvSTZGYW12WnhxdEFRTk5qOTBjZXhjMkduWERzYUN1MzBaWUpJMGhjT2tyazJFNThxNlZxQWh2UytiOHVwZlhYUzJLc0gxa0pHeXVnak5NNzMvanhIMW9YbklaL1l6VEtvODBvY0JjelpnVHlhMXpMa0J6bDhJSXFITUZzYzArLzMvZERxTzdmc0tmdmVxSFBqL1ZuQy96QmJqSzlHQTZ0NlhaZFlaekhFVWV0MmFObkM0YlJINVhDTDMxdnNSZFQwUTd0aWxHNG92c1dwbWdJS21sQ3RCRnNiSC9VbnJoYWVmeFRYSndCSEVoTXdXNkdMV2psWHhMMXVTNVUvNjdiZ3UwdGI5NW9zdzZYYTB2NkF0RkhmYXIrcVk3K2pQZUorRHVHUXFoeWV1d2gydnVldGtZZXBtbzBhTkZmTnovR0NEQzhsb0l1UFhmSU9qNDVTL2xkcEx1WkVBeVlMMmFjVHlZWUxpNFprKys2eXdINjQ3YzBEcUJYWWNWSjhnNmJSMGtobnBIdndJYTM1TXowck9uSmtIZFRCSnVzY0s1M3p1b3VYTURkY3VmSlN2S0tEeEcwdUEwVzVMWHZhdTcxZWRzNVJ2aHlKazN0a1BNUU0ydFc2bkxIZEViWERhVi9TRXRiOGV6cDdJT21hMWRscnBhVHFDbzZsMFVmeGNIQ2NtZUVlaFREZ3pPMDdORkZRcWVwTENhOVduZG9Fdm1PbDk1eU9ZZytZM2wrT0JCMDdEckRBNmNTTVVxWllwSURjTzdMRlJuMEU4UEsxVXRVVjNHNVE1VG1yaTdiQ1NHRTBkanJoRTgySzVySmdqSWxWV1FXOGVaNWpiUUpCbmdzcjQvZGNWR0NIdUttamJlb0JTNWJhK2NTUngreExIR1BLSXJmZFBJZ0hrb29walY1SU55bTJRcEtHTlBCYVdqNEJUYytnTzBKM3hjRTVVY3REOHdjZ3ZudElWTVRNL2tJaktUZlRFMzBkdnVHNWJuaUtFai9oMlZiaEdrTGtiK3hNMGJyMlpxV1VaR1VIeUlZVmNVdWs1aEdkcFBLRU1TL1p5bExnaWpodkdlc0gxUzczb0N0ZzR1V0V1aHVzaXZ6REZraGRrTFdkdXJ5OVAyUVV6RTlpbEEzNHE2anRxcjdXNW5XTm1qWmEzZGl1dmN5VVYrbC91dVlCMXNOZ2ZkQ2dheis5RUREN2loT1VHbzMwOVlGQ2pROEFOTG5qSklNZTZxb3lLRjVZYmoyZ0VoSFhrNG42R1NZMlcwYnhGNHVNVU1vNEg4UUhaL1VRSk50NGdocFlaek03TXlqYkJLTE1RUHZUVWVwTDhzSEZDREVJcVVFcjNsVWdYMW5qbW85b1VTMWV1WmM2QWM1Wmpnc3FGOEYvYldweUsxRUwrdy9sWDhXQWE5VzJTbmNBT0pXOGk0VHpFUFY1dDMzSktuWHBTUmwrMzhSQUFsU0NqZ3c0VDgzL2g3aDdqQ2N1M0ltS2Vsc09VajluRGo2cWl3a0pjangyM1ovM3Qxd2hETm1FMUQ4U0tCQzB5YVdzSmNXMG4raGgrYSsrYWwyemVwVGg0Tzg0STdJRm1mb3ZBS0Y0ejB2S1U3MUloMjgxUkJnMStJVTRiQzhsemo5bjhyeEJYdEhEaGpFYTFReHROcnI2cXEyTnJ0eitTTFp0eExDSXVURjg2OXpjU1ZBMktzQ1Jzd05iZm9Pb0h1eGo5QW4rR3BBMXkxcURLREpZRW1OeExjV3NKTkhOd3AybWQrYjI4OWFUcjVmTjV0S2huMjJtMnVqUm9YSi9ubXJPQmR1TnFNNFM0cDFxejVVZWxsTnBDeFY5REl3aWk1RzhDOThWWmtaZWlFNFQ0L2g5T3ZoY3hEcFBjWGJMVXBGc1VFSTVrVCtYMDJCbFBXM1l4bjFoZ3pFNFFkVmJwQ1NrRWQwNlo2WG9Lc3MxbXFKVzJXQ1BjN3lycHpxOGZML2RoWEhHS25JK08vQnRsRU44eVJFQjUwMFBkZ0JRUjRDVmZqN0YvbTRucEwwSHlrbnZBb0JGVTJvTm8xTFBlWEgybzhQK1BMMkxFWENPaVhzZDNnOFduZ0ZjVkFQODVxSjNBbE51cVdQQ1dhL0Ryb0N3THBsUUhWalNFWEhKN3NCWWwreEdZcFBpellFR0NpaEtWTW14a0JaeFB4eFk2VDR5d1d2a1dTeFNBUHFyZU5BOCtiQnExdmtiTVQ4QTBlNWxmVG5kYjFLMFdRM2RFNWR0VDJPd2RIMWhZVHdOTjF1RzQzQ0UrYVUrWFBxOFZaWXpBK05nZW9xV3hDQW9hOVdwZkZzZnB0d2NicHJDcVNoQ2FNd1NoWEhaeFFFUk5VaTE1TUFGazlwQzNNemFjeFVzOTN3SjlpVWNucWg5UTdUbmkzblJmMUxUbXNTWmhsdVJ0ZG9pTTRjK3A4TGczR3JzUFlGa1E2UDg1N1BGdFhucW9VY3ZRWWVZVDRUZlhkYk1kTWxmbUsvdTFmWXo3emlRYnFFRTB4c28vQWU5M3FZOW92Q1hjNTNvSmdHaHR6enoyVFI2L2VFZU93VmY5SFpFQ0xxc1R0eGIzWGFUREZXZ2N3RVZGN0tmVHJ5dzgyM1V6M3JldHAvS1pVYkEvbGpmSGg3OEFseUU0UG5ORUJCUlA0dE55ZWxBMEVvaHY1L2hhL3NDSHg5SUovcXF2OWRtNjQrVmVwOFhhVUZMb1N3clZzZWVDQS9XOTcrbXJNbjJ5eWZQNTR3TFRNM2lvZUVRTmJUR3JZYWJ2RDBvWWVvNUUvQ3FyMlRic295WUNFNU9uNVdoRXdzTlFKOU5CSzc1U1pNSHhCK3pKaEozYjY3ZndBRzFzL2l0dnVlS3g3OGV4VFZ0SFF5TitlNFJCMTdicm5HNkxkUW9tVHlDOVBTejJlUHFuRHhVUW9KMnJkZUNhZmhOWWc5eFJhNStqc2djWUU0Znh0QW1RcG5PWVFTb0dYMnJmRlRrNldwOHBmbi91NGtXVWVmc01yV2dSU0ZOeHgzWVdYK01oRHVnTjd0VjVycWlvdUxEb1ZLS04rMlkxL0dzdXZOcDVkdXp1N0RwcWZaTDBBQjZKV2dOcXE1MGo1QmxneGNUSnJkdTh5WGxjYjlhMEFXWG1NZytPa2kxWW1xbTNwUWJRMDNlMzA9PC94ZW5jOkNpcGhlclZhbHVlPjwveGVuYzpDaXBoZXJEYXRhPjwveGVuYzpFbmNyeXB0ZWREYXRhPjwvRW5jcnlwdGVkQXNzZXJ0aW9uPjwvc2FtbHA6UmVzcG9uc2U+";

        private const string AdfsSampleSamlPrettyPrinted = @"<samlp:Response ID=""_952f4b46-6169-4685-bf1e-9867fff3b0b7"" Version=""2.0"" IssueInstant=""2021-03-12T18:24:22.940Z"" Destination=""https://foresthealthtracker.dnr.wa.gov/Account/ADFSPost"" Consent=""urn:oasis:names:tc:SAML:2.0:consent:unspecified"" xmlns:samlp=""urn:oasis:names:tc:SAML:2.0:protocol"">
  <Issuer xmlns=""urn:oasis:names:tc:SAML:2.0:assertion"">http://ead.sts.wa.gov/adfs/services/trust</Issuer>
  <samlp:Status>
    <samlp:StatusCode Value=""urn:oasis:names:tc:SAML:2.0:status:Success"" />
  </samlp:Status>
  <EncryptedAssertion xmlns=""urn:oasis:names:tc:SAML:2.0:assertion"">
    <Assertion ID=""_a6dc0212-37c7-4229-8c57-b38a98376265"" IssueInstant=""2021-03-12T18:24:22.924Z"" Version=""2.0"">
      <Issuer>http://ead.sts.wa.gov/adfs/services/trust</Issuer>
      <ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">
        <ds:SignedInfo>
          <ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
          <ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" />
          <ds:Reference URI=""#_a6dc0212-37c7-4229-8c57-b38a98376265"">
            <ds:Transforms>
              <ds:Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" />
              <ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
            </ds:Transforms>
            <ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" />
            <ds:DigestValue>HPeuMWMDVqCDqOW8eTBFjamdqDpDrG2WybTARNECtug=</ds:DigestValue>
          </ds:Reference>
        </ds:SignedInfo>
        <ds:SignatureValue>Huhnd59+BsTLIILVs7IsVgDDBuudDWpoedHgbQTtvhwfFyPRAs8H/LbgoNVunp92IMMMOZ56cx/lPN4UYzOcupvof+o+/j1VsgaXt1nk6DeW+W/UV6rFnl+GSW7cdG92Rbjc/sc8a6fi7tq9xKb5xwU69iQ/c0BeJ+eGyL6Ha84xGUDUZYw6t0qy8Mo88WFMijH3tkTEv6N7BQbQ1IeEmPqNg/iQOu2FuK/yCrG2ueNc4iJy0jaXThSM49aLZxYmSCQ1Sg/y8icps7kZop4UcEmqgRf+ueF9C3cYjZVya08OeQNfYSMaM3PIaKsM6osv2wMEx4OmmmtpkqEjib7bfw==</ds:SignatureValue>
        <KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
          <ds:X509Data>
            <ds:X509Certificate>MIIDMTCCAhmgAwIBAgIQVz5UHyLFM6pDf3Sa4PlAtTANBgkqhkiG9w0BAQsFADAmMSQwIgYDVQQDExtUb2tlblNpZ25pbmctZWFkLnN0cy53YS5nb3YwHhcNMTIxMDAxMDgwMDAwWhcNMzIxMDAxMDgwMDAwWjAmMSQwIgYDVQQDExtUb2tlblNpZ25pbmctZWFkLnN0cy53YS5nb3YwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC7ksGEdMEmk3//GyFUuJN52dt3978jVlq9wB9OmWEYqWDmhGk3VoWDJ2iQVunlbFu6wxPcuYjXKH87lULvNNGwrvoVORVSg0uw65dMBK3dXbfdPbiLf/LAGkwzOmkfS8OEeX5LT4bfDFLCDuWThyHNGbiNJYkl4zSHdnmrk4kHUnB2WTJOVLVjFJ+uLAYLvTpOxVemZCCUYY4Nh7Ox6Tx5FhA0dUY8GPl0J4KYA+Zos+GPUtFann1MO0TZy3zAFI9xHyYj5rfT4VpX2rN0PHFChvXR/YZ70VMxHrAkbYTT2sXQvknF5QyuQAZ4HM1qgRcFoIkZ/iN68iths86My5/PAgMBAAGjWzBZMFcGA1UdAQRQME6AEF+Xbral6Q+ZpOl2ZO9+opmhKDAmMSQwIgYDVQQDExtUb2tlblNpZ25pbmctZWFkLnN0cy53YS5nb3aCEFc+VB8ixTOqQ390muD5QLUwDQYJKoZIhvcNAQELBQADggEBAFOatP6ptufqLN1jNggdiowMQoms+wr84ipKizpCtWFTXteGhmy0HVMiCE3u0Sd5xmG0l6qAf0Krmd0aY4R+wbtHEgq1/2s/9lmvY5mtKfiQ5I3+Vlmu2TzgCW3ruvhz1ptf6flwlVFfF5wm2RxaV5R/NKyvCGkphX40hIf1ghK7B+R/xC2M3e+zTSVGYr6DHBfrTDlG/cXdoqAJ3cG80l1FTK7hqZsJlu/9YAV+xu0K2lVfubu5z3G+zA/SaHqEgow2SJOmkBrfvc90d0wMMia4B9VwKnUIsoBYn0klUZmv+cZNBW14HyybL/B5G6X1Z/fgQHmvnjluVXT7XowXE0I=</ds:X509Certificate>
          </ds:X509Data>
        </KeyInfo>
      </ds:Signature>
      <Subject>
        <NameID Format=""urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress"">amy.ramsey@dnr.wa.gov</NameID>
        <SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"">
          <SubjectConfirmationData NotOnOrAfter=""2021-03-12T18:29:22.940Z"" Recipient=""https://foresthealthtracker.dnr.wa.gov/Account/ADFSPost"" />
        </SubjectConfirmation>
      </Subject>
      <Conditions NotBefore=""2021-03-12T18:24:22.924Z"" NotOnOrAfter=""2021-03-12T19:24:22.924Z"">
        <AudienceRestriction>
          <Audience>https://foresthealthtracker.dnr.wa.gov</Audience>
        </AudienceRestriction>
      </Conditions>
      <AttributeStatement>
        <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"">
          <AttributeValue>amy.ramsey@dnr.wa.gov</AttributeValue>
        </Attribute>
        <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/firstname"">
          <AttributeValue>Break 10:25 am</AttributeValue>
        </Attribute>
        <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/lastname"">
          <AttributeValue>RAMSEY</AttributeValue>
        </Attribute>
        <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn"">
          <AttributeValue>amy.ramsey@dnr.wa.gov</AttributeValue>
        </Attribute>
        <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/username"">
          <AttributeValue>arey490</AttributeValue>
        </Attribute>
        <Attribute Name=""http://schemas.xmlsoap.org/claims/Group"">
          <AttributeValue>G-S-DNR_WF_ForestHealth_Review</AttributeValue>
        </Attribute>
      </AttributeStatement>
      <AuthnStatement AuthnInstant=""2021-03-12T18:24:22.736Z"" SessionIndex=""_a6dc0212-37c7-4229-8c57-b38a98376265"">
        <AuthnContext>
          <AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport</AuthnContextClassRef>
        </AuthnContext>
      </AuthnStatement>
    </Assertion>
  </EncryptedAssertion>
</samlp:Response>";

        [Test]
        [Description("For this test we want the base64 and sample pretty printed to be the same so we can debug the tests more easily")]
        public void SampleBase64AndPrettyPrintAreTheSame()
        {
            var adfsSamlResponse = new ADFSSamlResponse();
            adfsSamlResponse.LoadXmlFromBase64(AdfsSampleSamlBase64Encoded);
            adfsSamlResponse.Decrypt();
            Assert.That(adfsSamlResponse.GetSamlAsPrettyPrintXml(), Is.EqualTo(AdfsSampleSamlPrettyPrinted));
        }

        [Test]
        public void TestAdfsSamlResponse()
        {
            var adfsSamlResponse = new ADFSSamlResponse();
            adfsSamlResponse.LoadXmlFromBase64(AdfsSampleSamlBase64Encoded);
            adfsSamlResponse.Decrypt();

            Assert.That(adfsSamlResponse.GetFirstName(), Is.EqualTo("Break 10:25 am"));
            Assert.That(adfsSamlResponse.GetLastName(), Is.EqualTo("RAMSEY"));
            Assert.That(adfsSamlResponse.GetEmail(), Is.EqualTo("amy.ramsey@dnr.wa.gov"));
            Assert.That(adfsSamlResponse.GetRoleGroups(), Is.EquivalentTo(new List<string>{ "G-S-DNR_WF_ForestHealth_Review" }));
        }
    }
}