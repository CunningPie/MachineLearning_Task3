# MachineLearning_Task3
на графе пользователей найти кластера методом Affinity Propagation. Сравнить эффективность полученных кластеров в задаче рекомендации мест (рядом с графом есть лог чекинов). Чекины для части пользователей прячем, а потом строим "рекомендации" на базе топа чекинов из кластера, куда попал пользователь. Качество рекомендации меряем по точности для первых 10 рекомендованных элементов. Если 10 набрать не удалось (в кластер не попало достаточно пользователей с чекинами) каждый недобранный айтем считаем за промах.

## Dataset

https://snap.stanford.edu/data/loc-Gowalla.html  

## Result

Из датасета был выделен блок из 1000 пользователей. Для них произошло разбиение на 2 кластера.

### User: 0, Cluster: 0

User Places: 420315 21714 18417 480992 25151 9263 9410 15326 23256 1221889

Top Places: 420315 21714 19542 9410 17208 9241 9246 42732 33793 34055

Accuracy: 0,3

### User: 1, Cluster: 0

User Places: 1500177 1493267 1441698 1436795 1431949 1423291 1422219 1414779 1404455 1399686

Top Places: 420315 21714 19542 9410 17208 9241 9246 42732 33793 34055

Accuracy: 0

### User: 615, Cluster: 1

User Places: 9410 212891 23372 5475294 16399 15810 27235 726095 166386 1247218

Top Places: 763638 53626 412917 330817 393164 25678 77910 496456 1015734 9961

Accuracy: 0

### User: 616, Cluster: 1

User Places: 132548 1715586 1128583 335962 1479250 1559009 694173 471501 861649 1461897

Top Places: 763638 53626 412917 330817 393164 25678 77910 496456 1015734 9961

Accuracy: 0
